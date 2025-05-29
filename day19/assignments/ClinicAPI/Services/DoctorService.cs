public class DoctorService : IDoctorService
{   
    private readonly IRepository<int,Doctor>  _doctorRepository;
    private readonly IRepository<int,Speciality> _specialityRepository;
    private readonly IRepository<int, DoctorSpeciality> _doctorSpecialityRepository;
    private readonly DoctorMapper _doctorMapper = new DoctorMapper();
    private readonly SpecialityMapper _specialityMapper = new SpecialityMapper();
    private readonly IOtherContextFunctionalities _otherContextFunctionalities;
    public DoctorService(IRepository<int, Doctor> doctorRepository,
                        IRepository<int, Speciality> specialityRepository,
                        IRepository<int, DoctorSpeciality> doctorSpecialityRepository,
                        IOtherContextFunctionalities otherContextFunctionalities)
    {
        _doctorRepository = doctorRepository;
        _specialityRepository = specialityRepository;
        _doctorSpecialityRepository = doctorSpecialityRepository;
        _otherContextFunctionalities = otherContextFunctionalities;
    }

    public async Task<Doctor> AddDoctor(DoctorAddRequestDTO doctorAddRequestDTO)
    {
        try
        {
            var doctor = _doctorMapper.MapDoctorAddRequestToDoctor(doctorAddRequestDTO);
            var newDoctor = await _doctorRepository.Add(doctor);
            if (newDoctor == null)
                throw new Exception("Could not add Doctor");
            if (doctorAddRequestDTO.Specialities.Count() > 0)
            {
                int[] specialityIds = await MapAndAddSpecialities(doctorAddRequestDTO.Specialities.ToList());
                for (int i = 0; i < specialityIds.Length; i++)
                {
                    var doctorSpeciality = _specialityMapper.MapDoctorSpecility(newDoctor.Id, specialityIds[i]);
                    doctorSpeciality = await _doctorSpecialityRepository.Add(doctorSpeciality);
                }
            }
            return newDoctor;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<int[]> MapAndAddSpecialities(List<SpecialityAddRequestDTO> specialityAddRequestDTOs)
    {
        int[] specialityIds = new int[specialityAddRequestDTOs.Count()];
        IEnumerable<Speciality> existingSpecialities = null;
        try
        {
            existingSpecialities = await _specialityRepository.GetAll();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        for (int i = 0; i < specialityAddRequestDTOs.Count(); i++)
        {
            Speciality speciality = null;
            if (existingSpecialities != null)
                speciality = existingSpecialities.FirstOrDefault(s => s.Name.ToLower() == specialityAddRequestDTOs[i].Name.ToLower());
            if (speciality == null)
            {
                speciality = _specialityMapper.SpecialityAddRequestToSpeciality(specialityAddRequestDTOs[i]);
                speciality = await _specialityRepository.Add(speciality);
            }
            specialityIds[i] = speciality.Id;
        }
        return specialityIds;
    }

    public async Task<Doctor> GetDoctByName(string name)
    {
        IEnumerable<Doctor> doctors;
        try
        {
            doctors = await _doctorRepository.GetAll();
            doctors = doctors.ToList();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        var doctor = doctors.FirstOrDefault(d => d.Name.ToLower() == name.ToLower());
        if (doctor == null)
            throw new Exception("Doctor with Name not found");
        return doctor;
        
    }

    public async Task<ICollection<DoctorsBySpecialityResponseDto>> GetDoctorsBySpeciality(string speciality)
    {
        var result = await _otherContextFunctionalities.GetDoctorsBySpeciality(speciality);
        return result;
    }
}