using assignment_1.Models.DTOs;

namespace assignment_1.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<UserLoginResponseDTO> Login(UserLoginRequest user);
    }
}