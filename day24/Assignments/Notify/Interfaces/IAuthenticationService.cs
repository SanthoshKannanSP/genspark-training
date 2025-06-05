using Notify.Models.DTOs;

namespace Notify.Interfaces;
public interface IAuthenticationService
{
    public Task<UserLoginResponseDTO> Login(UserLoginRequestDTO user);
}
