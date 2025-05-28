
public class DoctorService : IDoctorService
{   
    private readonly IRepository<int,Doctor>  _doctorRepository;
    private readonly ISpecialityService _specialityService;
    private readonly IDoctorSpecialityService _doctorSpecialityService;
    public DoctorService(IRepository<int, Doctor> doctorRepository,
                        ISpecialityService specialityService,
                        IDoctorSpecialityService doctorSpecialityService)
    {
        _doctorRepository = doctorRepository;
        _specialityService = specialityService;
        _doctorSpecialityService = doctorSpecialityService;
    }

    public async Task<Doctor> AddDoctor(DoctorAddRequestDTO doctor)
    {
        var newDoctor = await _doctorRepository.Add(new Doctor()
        {
            Name = doctor.Name,
            YearsOfExperience = doctor.YearsOfExperience,
            Status = "Active",
        });
        foreach (var speciality in doctor.Specialities)
        {
            var newSpeciality = await _specialityService.AddSpecialityIfNotExist(speciality);
            var newDoctorSpeciality = await _doctorSpecialityService.AddDoctorSpeciality(new DoctorSpeciality()
            {
                DoctorId = newDoctor.Id,
                SpecialityId = newSpeciality.Id,
                Doctor = newDoctor,
                Speciality = newSpeciality
            });
        }
        return newDoctor;
    }

    public async Task<Doctor> GetDoctByName(string name)
    {
        var doctors = await _doctorRepository.GetAll();
        var doctor = doctors.FirstOrDefault(d => d.Name == name);
        if (doctor != null)
            return doctor;
        throw new Exception("Doctor with given name not found");
    }

    public async Task<ICollection<Speciality>> GetSpecialitiesByDoctor(string doctorName)
    {
        var doctor = await GetDoctByName(doctorName);
        var specialities = await _doctorSpecialityService.GetSpecialitiesByDoctorId(doctor.Id);
        return specialities;
    }
}