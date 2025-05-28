public interface ISpecialityService
{
    public Task<Speciality> GetSpecialityByName(string Name);
    public Task<Speciality> AddSpecialityIfNotExist(SpecialityAddRequestDTO speciality);

    public Task<ICollection<Doctor>> GetDoctorsBySpeciality(string specialityName); 
}