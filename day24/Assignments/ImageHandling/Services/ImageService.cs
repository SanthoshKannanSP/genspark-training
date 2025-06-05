using ImageHandling.Interfaces;
using SkiaSharp;
namespace ImageHandling.Services;


public class ImageService : IImageService
{
    public byte[] ConvertToBytes(SKImage img)
    {
        using (var ms = new MemoryStream())
        {
            return img.Encode(SKEncodedImageFormat.Jpeg, 50).ToArray();
        }
    }

    public SKImage OpenImage(string path)
    {
        SKImage img = SKImage.FromEncodedData(path);
        return img;
    }

    public void SaveImage(IFormFile file)
    {
        using (var stream = new FileStream("savedImage.jpeg", FileMode.Create, FileAccess.Write))
        {
            var image = SKImage.FromEncodedData(file.OpenReadStream()).Encode(SKEncodedImageFormat.Jpeg, 50);
            image.SaveTo(stream);
        }
        return;
    }
}