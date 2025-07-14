namespace BlobDemo.Interfaces;

public interface IBlobStorageService
{
    public Task UploadFile(Stream fileStream, string fileName);

    public Task<Stream> DownloadFile(string fileName);
}