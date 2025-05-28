
public class DoctorSpecialityService : IDoctorSpecialityService
{
    private readonly IRepository<int, DoctorSpeciality> _doctorSpecialityRepository;
    public DoctorSpecialityService(IRepository<int, DoctorSpeciality> doctorSpecialityRepository)
    {
        _doctorSpecialityRepository = doctorSpecialityRepository;
    }
    public async Task<DoctorSpeciality> AddDoctorSpeciality(DoctorSpeciality doctorSpeciality)
    {
        return await _doctorSpecialityRepository.Add(doctorSpeciality);
    }

    public async Task<ICollection<Doctor>> GetDoctorsBySpecialityId(int specialityId)
    {
        try
        {
            var doctorSpecialities = await _doctorSpecialityRepository.GetAll();
            return doctorSpecialities.Where(ds => ds.SpecialityId == specialityId).Select(ds => ds.Doctor).ToList();
        }
        catch
        {
            return new List<Doctor>();
        }

    }

    public async Task<ICollection<Speciality>> GetSpecialitiesByDoctorId(int doctorId)
    {
        try
        {
            var doctorSpecialities = await _doctorSpecialityRepository.GetAll();
            return doctorSpecialities.Where(ds => ds.DoctorId == doctorId).Select(ds => ds.Speciality).ToList();
        }
        catch
        {
            return new List<Speciality>();
        }
    }
}