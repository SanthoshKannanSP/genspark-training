using Backend.Interfaces;
using Backend.Models.DTOs.News;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NewsController : ControllerBase
{
    private readonly INewsService _newsService;
    public NewsController(INewsService newsService)
    {
        _newsService = newsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetNewsList(int? page)
    {
        var response = await _newsService.GetPaginatedNews(page);
        return Ok(response);
    }

    [HttpGet]
    [Route("All")]
    public async Task<IActionResult> GetAllNews()
    {
        var response = await _newsService.GetAllNews();
        return Ok(response);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var response = await _newsService.GetNews(id);
        if (response == null) return NotFound();
        return Ok(response);
    }

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> Create(CreateNewsRequestDTO createNewsRequestDTO)
    {
        var response = await _newsService.CreateNews(createNewsRequestDTO);
        return Ok(response);
    }

    [HttpPost]
    [Route("Update")]
    public async Task<IActionResult> Update(EditNewsRequestDTO editNewsRequestDTO)
    {
        var response = await _newsService.EditNews(editNewsRequestDTO);
        if (response == null) return NotFound();
        return Ok(response);
    }

    [HttpDelete]
    [Route("Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _newsService.DeleteNews(id);
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpGet]
    [Route("Export/Csv")]
    public async Task<IActionResult> ExportCSV()
    {
        var csv = await _newsService.ExportToCsv();
        var fileName = $"NewsListing_{DateTime.Now:yyyyMMdd_HHmmss}.csv";

        Response.Headers.Add("Content-Disposition", $"attachment; filename={fileName}");
        Response.ContentType = "text/csv";

        return File(csv, "text/csv");
    }

    [HttpGet]
    [Route("Export/Excel")]
    public async Task<IActionResult> ExportExcel()
    {
        var excel = await _newsService.ExportToExcel();
        var fileName = $"NewsListing_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

        Response.Headers.Add("Content-Disposition", $"attachment; filename={fileName}");
        Response.ContentType = "application/excel";
        return File(excel, "application/excel");
    }

    [HttpGet("Image/{fileName}")]
    public IActionResult GetImage(string fileName)
    {
        var image = _newsService.GetNewsImage(fileName);

        if (image == null)
            return NotFound();

        return File(image, "image/jpeg");
    }
}
