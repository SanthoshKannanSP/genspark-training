using Microsoft.EntityFrameworkCore;
using TrainingPortalAPI.Context;
using TrainingPortalAPI.Interfaces;
using TrainingPortalAPI.Models;

namespace TrainingPortalAPI.Repositories;

public class VideoRepository : IVideoRepository
{
    private readonly AppDbContext _context;

    public VideoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Video video)
    {
        await _context.Videos.AddAsync(video);
        await _context.SaveChangesAsync();
    }

    public async Task<Video> GetByIdAsync(string id)
    {
        return await _context.Videos.FindAsync(id);
    }

    public async Task<IEnumerable<Video>> GetAllAsync()
    {
        return await _context.Videos.ToListAsync();
    }
}