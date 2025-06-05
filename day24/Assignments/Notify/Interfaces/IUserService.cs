using Notify.Models;
using Notify.Models.DTOs;

namespace Notify.Interfaces;

public interface IUserService
{
    public Task<User> addUser(UserAddRequestDTO requestDTO);
}