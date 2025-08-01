using Backend.Models;
using Backend.Models.DTOs;
using Backend.Models.DTOs.Category;
using X.PagedList;

namespace Backend.Interfaces;

public interface ICategoryService
{
    Task<PaginatedResponse<CategoryResponseDTO>> GetPaginatedCategories(int? page);
    Task<List<CategoryResponseDTO>> GetAllCategories();
    Task<CategoryResponseDTO> AddCategory(AddCategoryRequestDTO addCategoryRequestDto);
    Task<CategoryResponseDTO> GetCategoryById(int id);
    Task<CategoryResponseDTO> EditCategory(EditCategoryRequestDTO editCategoryRequestDTO);
    Task<bool> DeleteCategory(int id);
}