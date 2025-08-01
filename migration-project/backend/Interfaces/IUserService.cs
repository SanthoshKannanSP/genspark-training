using Backend.Models.DTOs.User;

namespace Backend.Interfaces;

public interface IUserService
{
    Task<List<UserResponseDTO>> GetUsers();
}