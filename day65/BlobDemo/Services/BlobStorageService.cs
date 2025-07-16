using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Storage.Blobs;
using BlobDemo.Interfaces;

namespace BlobDemo.Services;

public class BlobStorageService : IBlobStorageService
{
    private readonly IConfiguration _configuration;

    public BlobStorageService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private async Task<string> GetKeyVaultSecretAsync(string key)
    {
        var KeyVaultUrl = _configuration["AzureBlob:KeyVaultUrl"];
        SecretClient secretClient = new SecretClient(new Uri(KeyVaultUrl!), new DefaultAzureCredential());
        KeyVaultSecret secret = await secretClient.GetSecretAsync(key);
        return secret.Value;
    }

    public async Task<Stream?> DownloadFile(string fileName)
    {
        var blobSasUrl = await GetKeyVaultSecretAsync("BlobSasUrl");
        BlobContainerClient containerClinet = new (new Uri(blobSasUrl)); 
        var blobClient = containerClinet?.GetBlobClient(fileName);
        if (await blobClient!.ExistsAsync())
        {
            var downloadInfor = await blobClient.DownloadStreamingAsync();
            return downloadInfor.Value.Content;
        }
        return null;
    }

    public async Task UploadFile(Stream fileStream, string fileName)
    {
        var blobSasUrl = await GetKeyVaultSecretAsync("BlobSasUrl");
        BlobContainerClient containerClinet = new (new Uri(blobSasUrl)); 
        var blobClient = containerClinet.GetBlobClient(fileName);
        await blobClient.UploadAsync(fileStream,overwrite:true);
    }
}