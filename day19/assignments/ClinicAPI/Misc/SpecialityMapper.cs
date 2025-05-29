public class SpecialityMapper
{
    public Speciality SpecialityAddRequestToSpeciality(SpecialityAddRequestDTO specialityAddRequestDTO)
    {
        Speciality speciality = new();
        speciality.Name = specialityAddRequestDTO.Name;
        return speciality;
    }

    public DoctorSpeciality MapDoctorSpecility(int doctorId, int specialityId)
    {
        DoctorSpeciality doctorSpeciality = new();
        doctorSpeciality.DoctorId = doctorId;
        doctorSpeciality.SpecialityId = specialityId;
        return doctorSpeciality;
    }
}