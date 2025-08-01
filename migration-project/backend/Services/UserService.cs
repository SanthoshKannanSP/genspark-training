using Backend.Interfaces;
using Backend.Models;
using Backend.Models.DTOs.User;

namespace Backend.Services;

public class UserService : IUserService
{
    private readonly IRepository<int, User> _userRepository;
    public UserService(IRepository<int, User> userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<List<UserResponseDTO>> GetUsers()
    {
        var users = await _userRepository.GetAllAsync();
        var response = users.OrderBy(u => u.Username).Select(u => UserResponseDTO.MapFrom(u)).ToList();
        return response;
    }
}