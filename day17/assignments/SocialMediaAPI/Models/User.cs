using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

[PrimaryKey("Id")]
public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public ICollection<Tweet> Tweets { get; set; }

    public ICollection<Like> Likes { get; set; }
    [InverseProperty("FollowedUser")]
    public ICollection<UserFollow>? Followers { get; set; }
    [InverseProperty("FollowingUser")]
    public ICollection<UserFollow>? Following { get; set; }
}
