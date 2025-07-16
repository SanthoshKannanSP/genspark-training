using BlobDemo.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlobDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IBlobStorageService _blobStorageService;

        public FilesController(IBlobStorageService blobStorageService)
        {
            _blobStorageService  = blobStorageService;
        }
        [HttpGet]
        public async Task<ActionResult<Stream>> Download(string fileName)
        {
            var stream = await _blobStorageService.DownloadFile(fileName);
            if (stream == null) 
                return NotFound();
            return File(stream, "application/octet-stream", fileName);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Upload( IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file to upload");
            using var stream = file.OpenReadStream();
            await _blobStorageService.UploadFile(stream, file.FileName);
            return Ok("File uploaded");
        }
    }
}