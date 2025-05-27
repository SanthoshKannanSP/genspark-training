using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

[PrimaryKey("TweetId","HashtagName")]
public class TweetHashtag
{
    public int TweetId { get; set; }
    public string HashtagName { get; set; }

    [ForeignKey("TweetId")]
    public Tweet TaggedTweet { get; set; }

    [ForeignKey("HashtagName")]
    public Hashtag UsedHashtag { get; set; }

}