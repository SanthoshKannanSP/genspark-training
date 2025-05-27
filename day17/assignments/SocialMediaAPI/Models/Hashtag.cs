using Microsoft.EntityFrameworkCore;

[PrimaryKey("TagName")]
public class Hashtag
{
    public string TagName { get; set; }
    public ICollection<TweetHashtag> TaggedTweets { get; set; }
}