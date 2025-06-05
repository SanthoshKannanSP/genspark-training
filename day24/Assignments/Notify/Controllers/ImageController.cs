using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Notify.Interfaces;
using Notify.Misc;
using Notify.Models.DTOs;

namespace Notify.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class ImageController : ControllerBase
{
    private readonly IImageService _imageService;
    private readonly IHubContext<NotificationHub> _hubContext;
    public ImageController(IImageService imageService, IHubContext<NotificationHub> hubContext)
    {
        _imageService = imageService;
        _hubContext = hubContext;
    }

    [Authorize(Roles = "HR")]
    [HttpPost]
    public async Task<ActionResult<ImageUploadResponse>> UploadImage(IFormFile file)
    {
        var username = User.Identity?.Name;
        var imageId = _imageService.SaveImage(file);
        ImageUploadResponse response = new() { ImageId = imageId, Username = username };
        await _hubContext.Clients.All.SendAsync("ReceiveMessage", username, imageId);
        return Created("", response);
    }


    [HttpGet]
    public ActionResult DisplayImage(int imageId)
    {
        byte[] b = System.IO.File.ReadAllBytes($"Storage/{imageId}.jpeg");
        return File(b, "image/jpeg");
    }
}