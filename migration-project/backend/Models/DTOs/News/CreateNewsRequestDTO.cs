using Microsoft.AspNetCore.Mvc;

namespace Backend.Models.DTOs.News;

public class CreateNewsRequestDTO
{
    public int UserId { get; set; }
    public string Title { get; set; }
    public string ShortDescription { get; set; }
    public string Content { get; set; }
    public DateTime CreatedDate { get; set; }
    public int Status { get; set; }

    [FromForm]
    public IFormFile Image { get; set; }

    public static Models.News MapTo(CreateNewsRequestDTO createNewsRequestDTO)
    {
        return new Models.News()
        {
            Content = createNewsRequestDTO.Content,
            CreatedDate = createNewsRequestDTO.CreatedDate.ToUniversalTime(),
            Image = createNewsRequestDTO.Image.FileName,
            ShortDescription = createNewsRequestDTO.ShortDescription,
            Status = createNewsRequestDTO.Status,
            Title = createNewsRequestDTO.Title,
            UserId = createNewsRequestDTO.UserId
        };
    }
}