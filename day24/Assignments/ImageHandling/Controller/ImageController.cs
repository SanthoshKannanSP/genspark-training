using System.Drawing;
using ImageHandling.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SkiaSharp;

namespace ImageHandling.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class ImageController : ControllerBase
{
    private readonly IImageService _imageService;
    public ImageController(IImageService imageService)
    {
        _imageService = imageService;
    }

    [HttpGet]
    public ActionResult<byte[]> GetImage()
    {
        SKImage img = _imageService.OpenImage("kitty-cat-kitten-pet-45201.jpeg");
        return Ok(_imageService.ConvertToBytes(img));
    }

    [HttpPost]
    public ActionResult UploadImage(IFormFile file)
    {
        _imageService.SaveImage(file);
        return Ok();
    }
}