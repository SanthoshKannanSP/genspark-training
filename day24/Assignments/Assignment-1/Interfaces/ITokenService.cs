using assignment_1.Models;

namespace assignment_1.Interfaces
{
    public interface ITokenService
    {
        public Task<string> GenerateToken(User user);
    }
}