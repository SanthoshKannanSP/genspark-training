using assignment_1.Models;
using assignment_1.Models.DTOs;

namespace assignment_1.Interfaces
{
    public interface IPatientService
    {
        public Task<Patient> AddPatient(PatientAddRequestDTO requestDTO);

        public Task<List<Patient>> GetAllPatients();
    }
}