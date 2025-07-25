using System.Net;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using TrainingPortalAPI.Interfaces;
using TrainingPortalAPI.Models;

namespace TrainingPortalAPI.Services;

public class BlobService : IBlobService
{
    private readonly BlobContainerClient _containerClient;
    private readonly IVideoRepository _videoRepository;


    public BlobService(IConfiguration configuration, IVideoRepository videoRepository)
    {
        var sasUrl = configuration["Azure:BlobSASUrl"];
        _containerClient = new BlobContainerClient(new Uri(sasUrl));
        _videoRepository = videoRepository;
    }

    public async Task UploadVideoAsync(IFormFile file, string title, string description)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("Invalid file");

        var video = new Video
        {
            Title = title,
            Description = description,
            UploadDate = DateTime.UtcNow,
        };

        var blobClient = _containerClient.GetBlobClient(video.VideoId);

        using var stream = file.OpenReadStream();
        await blobClient.UploadAsync(stream, overwrite: true);

        await _videoRepository.AddAsync(video);
    }

    public async Task<Video> GetVideoDetailsAsync(string videoId)
    {
        return await _videoRepository.GetByIdAsync(videoId);
    }

    public async Task<Stream> StreamVideoAsync(string videoId)
    {
        var blobClient = _containerClient.GetBlobClient(videoId);

        if (!await blobClient.ExistsAsync())
            return null;

        return await blobClient.OpenReadAsync();
    }
    
    public async Task<IEnumerable<Video>> GetAllVideoDetailsAsync()
    {
        return await _videoRepository.GetAllAsync();
    }
}
