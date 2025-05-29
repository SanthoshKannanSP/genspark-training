public class DoctorMapper
{
    public Doctor MapDoctorAddRequestToDoctor(DoctorAddRequestDTO doctorAddRequestDTO)
    {
        Doctor doctor = new();
        doctor.Name = doctorAddRequestDTO.Name;
        doctor.YearsOfExperience = doctorAddRequestDTO.YearsOfExperience;
        return doctor;
    }
}