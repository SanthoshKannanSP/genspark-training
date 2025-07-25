using Azure.Storage.Blobs;
using BlobDemo.Interfaces;
using BlobDemo.Models;

namespace BlobDemo.Services;

public class BlobStorageService : IBlobStorageService
{
    private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<BlobStorageService> _logger;

    public BlobStorageService(IConfiguration configuration, IHttpClientFactory httpClientFactory,
            ILogger<BlobStorageService> logger)
    {
        _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
    }

    private async Task<BlobClient> GetBlobClientWithSas(string fileName)
        {
            string functionConnection = _configuration["Azure:FunctionConnection"];
            string functionCode = _configuration["Azure:FunctionCode"];

            if (functionConnection == null || functionCode == null)
                throw new Exception("Invalid Configuration");

            string functionUrl = $"{functionConnection}/api/generate-sas/{fileName}?code={functionCode}";
            var client = _httpClientFactory.CreateClient();
            var sasResponse = await client.GetAsync(functionUrl);
            if (!sasResponse.IsSuccessStatusCode)
            {
                var error = await sasResponse.Content.ReadAsStringAsync();
                _logger.LogError($"Failed to get SAS URL: {error}");
                throw new InvalidOperationException("Could not obtain SAS URL.");
            }

            var sasData = await sasResponse.Content.ReadFromJsonAsync<SasResponse>();
            if (sasData == null || string.IsNullOrWhiteSpace(sasData.sasUrl))
            {
                throw new InvalidOperationException("SAS URL response invalid.");
            }

            _logger.LogInformation($"SAS URL obtained: {sasData.sasUrl}");

            return new BlobClient(new Uri(sasData.sasUrl));
        }

    public async Task<Stream?> DownloadFile(string fileName)
    {
        var blobClient = await GetBlobClientWithSas(fileName);
        if (await blobClient!.ExistsAsync())
        {
            var downloadInfor = await blobClient.DownloadStreamingAsync();
            return downloadInfor.Value.Content;
        }
        return null;
    }

    public async Task UploadFile(Stream fileStream, string fileName)
    {
        var blobClient = await GetBlobClientWithSas(fileName);
        await blobClient.UploadAsync(fileStream,overwrite:true);
    }
}