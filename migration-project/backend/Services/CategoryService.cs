using Backend.Interfaces;
using Backend.Models;
using Backend.Models.DTOs;
using Backend.Models.DTOs.Category;
using X.PagedList;
using X.PagedList.Extensions;

namespace Backend.Services;

public class CategoryService : ICategoryService
{
    private readonly IRepository<int, Category> _categoryRepository;
    public CategoryService(IRepository<int, Category> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    public async Task<CategoryResponseDTO> AddCategory(AddCategoryRequestDTO addCategoryRequestDto)
    {
        var category = new Category()
        {
            Name = addCategoryRequestDto.Name
        };

        category = await _categoryRepository.AddAsync(category);

        var response = new CategoryResponseDTO()
        {
            CategoryId = category.CategoryId,
            Name = category.Name
        };
        return response;
    }

    public async Task<bool> DeleteCategory(int id)
    {
        await _categoryRepository.DeleteAsync(id);
        return true;
    }

    public async Task<CategoryResponseDTO> EditCategory(EditCategoryRequestDTO editCategoryRequestDTO)
    {
        var category = await _categoryRepository.GetByIdAsync(editCategoryRequestDTO.CategoryId);
        if (category == null)
            throw new Exception("Unable to update category");

        category.Name = editCategoryRequestDTO.Name;
        category = await _categoryRepository.UpdateAsync(category.CategoryId, category);
        var response = new CategoryResponseDTO()
        {
            CategoryId = category.CategoryId,
            Name = category.Name
        };
        return response;
    }

    public async Task<List<CategoryResponseDTO>> GetAllCategories()
    {
        var categories = await _categoryRepository.GetAllAsync();
        var response = categories.OrderBy(c => c.Name)
                            .Select(c => new CategoryResponseDTO() { CategoryId = c.CategoryId, Name = c.Name })
                            .ToList();
        return response;
    }

    public async Task<CategoryResponseDTO> GetCategoryById(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null)
            throw new Exception("Category not found");

        var response = new CategoryResponseDTO()
        {
            CategoryId = category.CategoryId,
            Name = category.Name
        };
        return response;
    }

    public async Task<PaginatedResponse<CategoryResponseDTO>> GetPaginatedCategories(int? page)
    {
        int pageNumber = page ?? 1;
        int pageSize = 5;
        var categories = await _categoryRepository.GetAllAsync();
        var response = categories.OrderBy(c => c.Name)
                            .Select(c => new CategoryResponseDTO() { CategoryId = c.CategoryId, Name = c.Name })
                            .ToPagedList(pageNumber, pageSize);
        return new PaginatedResponse<CategoryResponseDTO>()
        {
            Items = response,
            Page = response.PageNumber,
            TotalPage = response.PageCount
        };
    }
}