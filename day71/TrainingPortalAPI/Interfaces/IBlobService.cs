using Microsoft.AspNetCore.Mvc;
using TrainingPortalAPI.Models;

namespace TrainingPortalAPI.Interfaces;

public interface IBlobService
{
    public Task<IEnumerable<Video>> GetAllVideoDetailsAsync();
    public Task<Video> GetVideoDetailsAsync(string fileName);
    public Task<Stream> StreamVideoAsync(string fileName);
    public Task UploadVideoAsync(IFormFile file, string title, string description);

}