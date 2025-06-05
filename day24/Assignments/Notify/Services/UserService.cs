using Notify.Interfaces;
using Notify.Models;
using Notify.Models.DTOs;

namespace Notify.Services;

public class UserService : IUserService
{
    private readonly IRepository<string, User> _userRepository;
    private readonly IEncryptionService _encryptionService;
    public UserService(IRepository<string, User> userRepository, IEncryptionService encryptionService)
    {
        _userRepository = userRepository;
        _encryptionService = encryptionService;
    }

    public async Task<User> addUser(UserAddRequestDTO requestDTO)
    {
        var encryptedData = await _encryptionService.EncryptData(new EncryptModel
        {
            Data = requestDTO.Password
        });

        User user = new()
        {
            Username = requestDTO.Username,
            Role = requestDTO.Role,
            Password = encryptedData.EncryptedData,
            HashKey = encryptedData.HashKey
        };

        user = await _userRepository.Add(user);
        return user;
    }
}