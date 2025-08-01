using Microsoft.AspNetCore.Mvc;

namespace Backend.Models.DTOs.News;

public class EditNewsRequestDTO
{
    public int NewsId { get; set; }
    public string Title { get; set; }
    public string ShortDescription { get; set; }
    public string Content { get; set; }
    public DateTime CreatedDate { get; set; }
    public int Status { get; set; }

    [FromForm]
    public IFormFile? Image { get; set; } = null;

    public static Models.News Combine(EditNewsRequestDTO editNewsRequestDTO, Models.News news)
    {
        news.Title = editNewsRequestDTO.Title;
        news.ShortDescription = editNewsRequestDTO.ShortDescription;
        if (editNewsRequestDTO.Image != null)
            news.Image = editNewsRequestDTO.Image.FileName;
        news.Content = editNewsRequestDTO.Content;
        news.CreatedDate = editNewsRequestDTO.CreatedDate;
        news.Status = editNewsRequestDTO.Status;
        return news;
    }
}