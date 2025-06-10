using AttendanceApi.Interfaces;
using AttendanceApi.Models;
using AttendanceApi.Models.DTOs;

namespace AttendanceApi.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IRepository<string, User> _userRepository;
    private readonly IEncryptionService _encryptionService;
    private readonly ITokenService _tokenService;
    public AuthenticationService(IRepository<string, User> userRepository, IEncryptionService encryptionService, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _encryptionService = encryptionService;
        _tokenService = tokenService;
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
        var response = new LoginResponseDTO()
        {
            Username = user.Username,
            Token = token
        };
        return response;
    }
}