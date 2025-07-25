using System.ComponentModel.DataAnnotations;

namespace TrainingPortalAPI.Models;

public class Video
{
    [Key]
    public string VideoId { get; set; } = GenerateRandomId();
    public string Title { get; set; } = string.Empty;
    public DateTime UploadDate { get; set; }
    public string Description { get; set; } = string.Empty;

    private static string GenerateRandomId()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        var result = new char[6];
        for (int i = 0; i < 6; i++)
        {
            result[i] = chars[random.Next(chars.Length)];
        }
        return new string(result);
    }
}