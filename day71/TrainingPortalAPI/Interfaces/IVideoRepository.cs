using TrainingPortalAPI.Models;

namespace TrainingPortalAPI.Interfaces;

public interface IVideoRepository
{
    public Task AddAsync(Video entity);
    Task<Video> GetByIdAsync(string id);
    Task<IEnumerable<Video>> GetAllAsync();
}