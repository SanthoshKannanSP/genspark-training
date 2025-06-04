using assignment_1.Models;
using assignment_1.Models.DTOs;

namespace assignment_1.Interfaces
{
    public interface IDoctorService
    {
        public Task<Doctor> AddDoctor(DoctorAddRequestDTO doctor);

        public Task<List<Doctor>> GetAllDoctors();
    }
}