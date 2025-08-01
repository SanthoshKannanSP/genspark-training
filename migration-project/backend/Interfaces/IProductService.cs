using Backend.Models.DTOs;
using Backend.Models.DTOs.Product;
using X.PagedList;

namespace Backend.Interfaces;

public interface IProductService
{
    Task<PaginatedResponse<ProductResponseDTO>> GetPaginatedProducts(int? page, int? categoryId);
    Task<ProductResponseDTO?> GetProduct(int id);
    byte[]? GetProductImage(string fileName);
}