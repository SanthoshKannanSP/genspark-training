using Microsoft.AspNetCore.Mvc;
using TrainingPortalAPI.Interfaces;
using TrainingPortalAPI.Models.DTOs;

namespace TrainingPortalAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VideoController : ControllerBase
{
    private readonly IBlobService _blobService;

    public VideoController(IBlobService blobService)
    {
        _blobService = blobService;
    }

    [HttpPost("upload")]
    public async Task<ActionResult<UploadResponseDTO>> Upload(IFormFile file, [FromForm] string title, [FromForm] string description)
    {
        System.Console.WriteLine("Hello Worlds");
        await _blobService.UploadVideoAsync(file, title, description);
        return Ok(new UploadResponseDTO(){videoId="abcdef"});
    }

    [HttpGet("{videoId}/details")]
    public async Task<IActionResult> GetVideoDetails(string videoId)
    {
        var video = await _blobService.GetVideoDetailsAsync(videoId);

        if (video == null)
            return NotFound("Video metadata not found.");

        return Ok(video);
    }

    [HttpGet("{videoId}/stream")]
    public async Task<IActionResult> StreamVideo(string videoId)
    {
        var stream = await _blobService.StreamVideoAsync(videoId);

        if (stream == null)
            return NotFound("Video not found in storage.");

        return File(stream, "video/mp4", enableRangeProcessing: true);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllVideos()
    {
        var videos = await _blobService.GetAllVideoDetailsAsync();

        return Ok(videos);
    }
}