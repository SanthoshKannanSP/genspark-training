public class OtherContextFunctionalities : IOtherContextFunctionalities
    {
        private readonly ClinicContext _clinicContext;

        public OtherContextFunctionalities(ClinicContext clinicContext)
        {
            _clinicContext = clinicContext;
        }

        public async Task<ICollection<DoctorsBySpecialityResponseDto>> GetDoctorsBySpeciality(string specilaity)
        {
            var result = await _clinicContext.GetDoctorsBySpeciality(specilaity);
            return result;
        }
    }