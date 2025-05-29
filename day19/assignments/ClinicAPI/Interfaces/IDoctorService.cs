public interface IDoctorService
{
    public Task<Doctor> GetDoctByName(string name);
    public Task<Doctor> AddDoctor(DoctorAddRequestDTO doctor);

    public Task<ICollection<DoctorsBySpecialityResponseDto>> GetDoctorsBySpeciality(string speciality);
}