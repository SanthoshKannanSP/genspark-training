

public class SpecialityService : ISpecialityService
{
    private readonly IRepository<int, Speciality> _specialityRepository;
    private readonly IDoctorSpecialityService _doctorSpecialityService;

    public SpecialityService(IRepository<int, Speciality> specialityRepository, IDoctorSpecialityService doctorSpecialityService)
    {
        _specialityRepository = specialityRepository;
        _doctorSpecialityService = doctorSpecialityService;
    }

    public async Task<Speciality> AddSpecialityIfNotExist(SpecialityAddRequestDTO speciality)
    {
        try
        {
            var existingSpeciality = await GetSpecialityByName(speciality.Name);
            return existingSpeciality;
        }
        catch
        {
            var newSpeciality = new Speciality() { Name = speciality.Name, Status = "Active" };
            return await _specialityRepository.Add(newSpeciality);
        }
    }

    public async Task<ICollection<Doctor>> GetDoctorsBySpeciality(string specialityName)
    {
        var speciality = GetSpecialityByName(specialityName);
        var doctors = await _doctorSpecialityService.GetDoctorsBySpecialityId(speciality.Id);
        return doctors;
    }

    public async Task<Speciality> GetSpecialityByName(string Name)
    {
        var specialities = await _specialityRepository.GetAll();
        var speciality = specialities.FirstOrDefault(s => s.Name == Name);
        if (speciality != null)
            return speciality;

        throw new Exception("Speciality Not Found");
    }
}