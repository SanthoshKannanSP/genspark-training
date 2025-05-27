using Microsoft.EntityFrameworkCore;

public class TwitterContext : DbContext
{
    public TwitterContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<User> UsersDb { get; set; }
    public DbSet<Tweet> TweetsDb { get; set; }
    public DbSet<Like> LikesDb { get; set; }
    public DbSet<Hashtag> HashtagsDb { get; set; }
    public DbSet<UserFollow> UserFollowsDb { get; set; }
    public DbSet<TweetHashtag> TweetHashtagsDb { get; set; }
}
