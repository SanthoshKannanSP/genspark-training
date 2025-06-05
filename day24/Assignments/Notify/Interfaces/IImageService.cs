using SkiaSharp;

namespace Notify.Interfaces;

public interface IImageService
{
    public int SaveImage(IFormFile file);
}