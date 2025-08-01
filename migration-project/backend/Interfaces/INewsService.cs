using Backend.Models;
using Backend.Models.DTOs;
using Backend.Models.DTOs.News;
using X.PagedList;

namespace Backend.Interfaces;

public interface INewsService
{
    Task<PaginatedResponse<NewsResponseDTO>> GetPaginatedNews(int? page);
    Task<NewsResponseDTO?> GetNews(int id);
    Task<NewsResponseDTO> CreateNews(CreateNewsRequestDTO createNewsRequestDTO);
    Task<NewsResponseDTO?> EditNews(EditNewsRequestDTO editNewsRequestDTO);
    Task<bool> DeleteNews(int id);
    Task<byte[]> ExportToCsv();
    Task<byte[]> ExportToExcel();
    Task<List<NewsResponseDTO>> GetAllNews();

    byte[]? GetNewsImage(string fileName);

}