using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

[PrimaryKey("Id")]
public class Tweet
{
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime PostedOn { get; set; }
    public int UserId { get; set; }

    [ForeignKey("UserId")]
    public User PostedBy { get; set; }
    public ICollection<Like> Likes { get; set; }
    public ICollection<TweetHashtag> UsedHashtags { get; set; }
}