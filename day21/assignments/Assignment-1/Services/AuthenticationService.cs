using assignment_1.Interfaces;
using assignment_1.Models;
using assignment_1.Models.DTOs;
using Microsoft.Extensions.Logging;

namespace assignment_1.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ITokenService _tokenService;
        private readonly IEncryptionService _encryptionService;
        private readonly IRepository<int, User> _userRepository;
        private readonly IRepository<int, Patient> _patientRepository;

        private readonly ILogger<AuthenticationService> _logger;

        public AuthenticationService(ITokenService tokenService,
                                    IEncryptionService encryptionService,
                                    IRepository<int, User> userRepository,
                                    IRepository<int,Patient> patientRepository,
                                    ILogger<AuthenticationService> logger)
        {
            _tokenService = tokenService;
            _encryptionService = encryptionService;
            _userRepository = userRepository;
            _patientRepository = patientRepository;
            _logger = logger;
        }
        public async Task<UserLoginResponseDTO> Login(UserLoginRequest user)
        {
            var dbUsers = await _userRepository.GetAll();
            var dbUser = dbUsers.FirstOrDefault(u => u.Username == user.Username);
            if (dbUser == null)
            {
                _logger.LogCritical("User not found");
                throw new Exception("No such user");
            }
            var encryptedData = await _encryptionService.EncryptData(new EncryptModel
            {
                Data = user.Password,
                HashKey = dbUser.HashKey
            });
            for (int i = 0; i < encryptedData.EncryptedData.Length; i++)
            {
                if (encryptedData.EncryptedData[i] != dbUser.Password[i])
                {
                    _logger.LogError("Invalid login attempt");
                    throw new Exception("Invalid password");
                }
            }
            var token = await _tokenService.GenerateToken(dbUser);
            return new UserLoginResponseDTO
            {
                Username = user.Username,
                Token = token,
            };
        }

        public async Task<UserLoginResponseDTO> PatientLogin(UserLoginRequest user)
        {
            var dbUsers = await _patientRepository.GetAll();
            var dbUser = dbUsers.FirstOrDefault(u => u.Name == user.Username);
            if (dbUser == null)
            {
                _logger.LogCritical("User not found");
                throw new Exception("No such user");
            }
            var encryptedData = await _encryptionService.EncryptData(new EncryptModel
            {
                Data = user.Password,
                HashKey = dbUser.HashKey
            });
            for (int i = 0; i < encryptedData.EncryptedData.Length; i++)
            {
                if (encryptedData.EncryptedData[i] != dbUser.PhoneNumber[i])
                {
                    _logger.LogError("Invalid login attempt");
                    throw new Exception("Invalid password");
                }
            }
            var token = await _tokenService.GeneratePatientToken(dbUser);
            return new UserLoginResponseDTO
            {
                Username = user.Username,
                Token = token,
            };
        }
    }
}