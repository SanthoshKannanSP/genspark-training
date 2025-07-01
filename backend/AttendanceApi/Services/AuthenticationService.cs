using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AttendanceApi.Interfaces;
using AttendanceApi.Models;
using AttendanceApi.Models.DTOs;
using Microsoft.IdentityModel.Tokens;

namespace AttendanceApi.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IRepository<string, User> _userRepository;
    private readonly IEncryptionService _encryptionService;
    private readonly ITokenService _tokenService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _configuration;

    public AuthenticationService(IRepository<string, User> userRepository, IEncryptionService encryptionService, ITokenService tokenService, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _encryptionService = encryptionService;
        _tokenService = tokenService;
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
    }
    public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
    {
        var user = await _userRepository.Get(loginRequestDTO.Username);
        if (user == null)
            throw new Exception("User not found");
        var encryptedData = _encryptionService.EncryptData(new EncryptModel()
        {
            Data = loginRequestDTO.Password,
            HashKey = user.HashKey
        });
        for (int i = 0; i < user.Password.Length; i++)
        {
            if (encryptedData.EncryptedData[i] != user.Password[i])
                throw new Exception("Invalid Username or Password");
        }
        var token = _tokenService.GenerateToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        await _userRepository.Update(user.Username, user);

        var response = new LoginResponseDTO()
        {
            Username = user.Username,
            Token = token,
            RefreshToken = refreshToken
        };
        return response;
    }

    public async Task<RefreshTokenResponseDTO> RefreshToken(RefreshTokenRequestDTO request)
    {
        var principal = GetPrincipalFromExpiredToken(request.AccessToken);
        if (principal == null)
            throw new Exception("Token not found");

        var username = principal.FindFirst(ClaimTypes.Name)?.Value;
        if (username == null)
            throw new Exception("Username not found");
        var user = await _userRepository.Get(username);
        if (user == null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            throw new Exception("Invalid refresh token");
        }

        var newToken = _tokenService.GenerateToken(user);
        var newRefreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        await _userRepository.Update(user.Username, user);

        return new RefreshTokenResponseDTO
        {
            AccessToken = newToken
        };
    }

    public async Task Logout()
    {
        var username = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
        if (string.IsNullOrEmpty(username))
            throw new Exception("Username not found");
        var user = await _userRepository.Get(username);
        if (user != null)
        {
            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = null;
            await _userRepository.Update(user.Username, user);
        }
        else
        {
            throw new Exception("User not found");
        }
    }
    
    private ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Keys:JwtTokenKey"])),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256))
                return null;

            return principal;
        }
        catch
        {
            return null;
        }
    }
}