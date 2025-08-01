namespace Backend.Models.DTOs.User;

public class UserResponseDTO
{
    public int UserId { get; set; }
    public string Username { get; set; } = "";

    public static UserResponseDTO MapFrom(Models.User user)
    {
        return new UserResponseDTO()
        {
            UserId = user.UserId,
            Username = user.Username
        };
    }
}