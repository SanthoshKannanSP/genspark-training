using Backend.Interfaces;
using Backend.Models.DTOs;
using Backend.Models.DTOs.Category;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetPaginatedCategories(int? page)
    {
        var response = await _categoryService.GetPaginatedCategories(page);
        return Ok(response);
    }

    [HttpGet]
    [Route("All")]
    public async Task<ActionResult<List<CategoryResponseDTO>>> GetAllCategories()
    {
        var response = await _categoryService.GetAllCategories();
        return Ok(response);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<CategoryResponseDTO>> GetCategory(int id)
    {
        var response = await _categoryService.GetCategoryById(id);
        return Ok(response);
    }

    [HttpPost]
    [Route("Create")]
    public async Task<ActionResult<CategoryResponseDTO>> CreateCategory(AddCategoryRequestDTO addCategoryRequestDTO)
    {
        var response = await _categoryService.AddCategory(addCategoryRequestDTO);
        return Ok(response);
    }

    [HttpPost]
    [Route("Edit")]
    public async Task<ActionResult<CategoryResponseDTO>> EditCategory(EditCategoryRequestDTO editCategoryRequestDTO)
    {
        var response = await _categoryService.EditCategory(editCategoryRequestDTO);
        return Ok(response);
    }

    [HttpDelete]
    [Route("Delete/{id}")]
    public async Task<ActionResult> DeleteCategory(int id)
    {
        var response = await _categoryService.DeleteCategory(id);
        return Ok(response);
    }
}