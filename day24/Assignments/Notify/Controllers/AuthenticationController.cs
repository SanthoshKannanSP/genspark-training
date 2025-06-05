using Notify.Interfaces;
using Notify.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Notify.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService, ILogger<AuthenticationController> logger)
    {
        _authenticationService = authenticationService;
    }
    
    [HttpPost]
    public async Task<ActionResult<UserLoginResponseDTO>> UserLogin(UserLoginRequestDTO loginRequest)
    {
        try
        {
            var result = await _authenticationService.Login(loginRequest);
            return Ok(result);
        }
        catch (Exception e)
        {
            return Unauthorized(e.Message);
        }
    }
}