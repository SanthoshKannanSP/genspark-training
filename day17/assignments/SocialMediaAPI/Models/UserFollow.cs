using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

[PrimaryKey("FollowedId","FollowerId")]
public class UserFollow
{
    public int UserId { get; set; }
    public int FollowingId { get; set; }

    [ForeignKey("FollowedId")]
    public User FollowedUser { get; set; }

    [ForeignKey("FollowerId")]
    public User FollowingUser { get; set; }
}