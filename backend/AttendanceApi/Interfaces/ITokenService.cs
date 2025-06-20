using AttendanceApi.Models;

namespace AttendanceApi.Interfaces;

public interface ITokenService
{
    public string GenerateToken(User user);
    public string GenerateRefreshToken();
    public bool ValidateRefreshToken(User user, string refreshToken);
    public void InvalidateRefreshToken(User user);
}