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

    [HttpPost("Refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequestDTO dto)
    {
        try
        {
            var result = await _authenticationService.RefreshToken(dto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }

    [HttpPost("Logout")]
    public async Task<IActionResult> Logout([FromBody] string username)
    {
        await _authenticationService.Logout(username);
        return Ok("Logged out successfully");
    }
}