using SkiaSharp;
namespace ImageHandling.Interfaces;


public interface IImageService
{
    public byte[] ConvertToBytes(SKImage img);

    public SKImage OpenImage(string path);
    public void SaveImage(IFormFile file);
}