using System.Text;
using System.Text.Encodings.Web;
using Backend.Interfaces;
using Backend.Models;
using Backend.Models.DTOs;
using Backend.Models.DTOs.News;
using ClosedXML.Excel;
using X.PagedList;
using X.PagedList.Extensions;

namespace Backend.Services;

public class NewsService : INewsService
{
    private readonly IRepository<int, News> _newsRepository;
    public NewsService(IRepository<int, News> newRepository)
    {
        _newsRepository = newRepository;
    }

    public async Task<NewsResponseDTO> CreateNews(CreateNewsRequestDTO createNewsRequestDTO)
    {
        var news = CreateNewsRequestDTO.MapTo(createNewsRequestDTO);
        news = await _newsRepository.AddAsync(news);

        var file = createNewsRequestDTO.Image;
        string fileName = Path.GetFileName(file.FileName);
        string savePath = Path.Combine("Media/Images/News", fileName);

        using (var stream = new FileStream(savePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        news = await _newsRepository.GetByIdAsync(news.NewsId);
        var response = NewsResponseDTO.MapFrom(news);
        return response;
    }

    public async Task<bool> DeleteNews(int id)
    {
        await _newsRepository.DeleteAsync(id);
        return true;
    }

    public async Task<NewsResponseDTO?> EditNews(EditNewsRequestDTO editNewsRequestDTO)
    {
        var news = await _newsRepository.GetByIdAsync(editNewsRequestDTO.NewsId);
        if (news == null)
            return null;

        if (editNewsRequestDTO.Image != null)
        {
            var file = editNewsRequestDTO.Image;
            string fileName = Path.GetFileName(file.FileName);
            string savePath = Path.Combine("Media/Images/News", fileName);

            using (var stream = new FileStream(savePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }

        news = EditNewsRequestDTO.Combine(editNewsRequestDTO, news);
        news = await _newsRepository.UpdateAsync(news.NewsId, news);
        news = await _newsRepository.GetByIdAsync(news.NewsId);
        var response = NewsResponseDTO.MapFrom(news);
        return response;
    }

    public async Task<byte[]> ExportToCsv()
    {
        var news = await _newsRepository.GetAllAsync();
        news = news.OrderBy(n => n.NewsId);
        var sb = new StringBuilder();
        sb.AppendLine("\"NewsId\",\"Title\",\"ShortDescription\",\"CreatedDate\",\"Status\"");
        foreach (var n in news)
        {
            sb.AppendLine($"\"{n.NewsId}\",\"{n.Title}\",\"{n.ShortDescription}\",\"{n.CreatedDate:yyyy-MM-dd}\",\"{n.Status}\"");
        }
        return Encoding.UTF8.GetBytes(sb.ToString());
    }

    public async Task<byte[]> ExportToExcel()
    {
        var newsList = await _newsRepository.GetAllAsync();
        newsList = newsList.OrderBy(n => n.NewsId);
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("News");
        worksheet.Cell(1, 1).Value = "NewsId";
        worksheet.Cell(1, 2).Value = "Title";
        worksheet.Cell(1, 3).Value = "ShortDescription";
        worksheet.Cell(1, 4).Value = "CreatedDate";
        worksheet.Cell(1, 5).Value = "Status";

        int row = 2;
        foreach (var news in newsList)
        {
            worksheet.Cell(row, 1).Value = news.NewsId;
            worksheet.Cell(row, 2).Value = news.Title;
            worksheet.Cell(row, 3).Value = news.ShortDescription;
            worksheet.Cell(row, 4).Value = news.CreatedDate.Value.ToString("dd-MM-yyyy");
            worksheet.Cell(row, 5).Value = news.Status == 1 ? "Active" : "Inactive";
            row++;
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }

    public async Task<NewsResponseDTO?> GetNews(int id)
    {
        var news = await _newsRepository.GetByIdAsync(id);
        if (news == null)
            return null;
        var response = NewsResponseDTO.MapFrom(news);
        return response;
    }

    public async Task<PaginatedResponse<NewsResponseDTO>> GetPaginatedNews(int? page)
    {
        int pageNumber = page ?? 1;
        int pageSize = 2;
        var news = await _newsRepository.GetAllAsync();
        var response = news.OrderByDescending(n => n.NewsId)
                            .Select(n => NewsResponseDTO.MapFrom(n))
                            .ToPagedList(pageNumber, pageSize);
        return new PaginatedResponse<NewsResponseDTO>()
        {
            Items = response,
            Page = response.PageNumber,
            TotalPage = response.PageCount
        };
    }

    public async Task<List<NewsResponseDTO>> GetAllNews()
    {
        var news = await _newsRepository.GetAllAsync();
        var response = news.OrderBy(n => n.NewsId).Select(n => NewsResponseDTO.MapFrom(n)).ToList();
        return response;
    }

    public byte[]? GetNewsImage(string fileName)
    {
        string imagePath = Path.Combine("Media/Images/News", fileName);

        if (!File.Exists(imagePath))
            return null;

        var imageData = File.ReadAllBytes(imagePath);
        return imageData;
    }
}