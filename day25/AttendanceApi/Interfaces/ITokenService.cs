using AttendanceApi.Models;

namespace AttendanceApi.Interfaces;

public interface ITokenService
{
    public string GenerateToken(User user);
}