using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Notify.Interfaces;
using Notify.Misc;
using SkiaSharp;

namespace Notify.Services;

public class ImageService : IImageService
{
    private static int seq = 1;
    public int SaveImage(IFormFile file)
    {
        using (var stream = new FileStream($"Storage/{seq}.jpeg", FileMode.Create, FileAccess.Write))
        {
            var image = SKImage.FromEncodedData(file.OpenReadStream()).Encode(SKEncodedImageFormat.Jpeg, 50);
            image.SaveTo(stream);
        }
        return seq++;
    }
}