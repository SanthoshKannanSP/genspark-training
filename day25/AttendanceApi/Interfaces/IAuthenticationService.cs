using AttendanceApi.Models.DTOs;

namespace AttendanceApi.Interfaces;

public interface IAuthenticationService
{
    public Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
}