using assignment_1.Models;
using assignment_1.Models.DTOs;

namespace assignment_1.Interfaces
{
    public interface IUserService
    {
        public Task<User> AddUser(UserAddRequestDTO requestDTO);

        public Task<List<User>> GetAllUsers();
    }
}