public interface IDoctorSpecialityService
{
    public Task<DoctorSpeciality> AddDoctorSpeciality(DoctorSpeciality doctorSpeciality);
    public Task<ICollection<Doctor>> GetDoctorsBySpecialityId(int doctorId);
    public Task<ICollection<Speciality>> GetSpecialitiesByDoctorId(int specialityId);
}