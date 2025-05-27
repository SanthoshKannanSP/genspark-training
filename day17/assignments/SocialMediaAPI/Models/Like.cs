using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

[PrimaryKey("Id")]
public class Like
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int TweetId { get; set; }

    [ForeignKey("TweetId")]
    public Tweet LikedTweet { get; set; }

    [ForeignKey("UserId")]
    public User LikedBy { get; set; }
}