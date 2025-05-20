public class Post
{
    public string Caption { get; set; }
    public int Likes { get; set; }
    
    public Post(string caption, int likes)
    {
        Caption = caption;
        Likes = likes;
    }
}