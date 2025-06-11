using System.Threading.Tasks;
using AttendanceApi.Interfaces;
using AttendanceApi.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceApi.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost]
    public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
    {
        var response = await _authenticationService.Login(loginRequestDTO);
        return response;
    }
}