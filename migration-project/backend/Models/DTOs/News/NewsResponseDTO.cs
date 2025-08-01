namespace Backend.Models.DTOs.News;

public class NewsResponseDTO
{
    public int NewsId { get; set; }
    public string Title { get; set; }
    public string ShortDescription { get; set; }
    public string Image { get; set; }
    public string Content { get; set; }
    public DateTime? CreatedDate { get; set; }
    public int? Status { get; set; }
    public int UserId { get; set; }
    public string Username { get; set; }
    public static NewsResponseDTO MapFrom(Models.News news)
    {
        return new NewsResponseDTO()
        {
            NewsId = news.NewsId,
            Content = news.Content,
            CreatedDate = news.CreatedDate,
            Image = news.Image,
            ShortDescription = news.ShortDescription,
            Status = news.Status,
            Title = news.Title,
            UserId = news.User.UserId,
            Username = news.User.Username
        };
    }
}