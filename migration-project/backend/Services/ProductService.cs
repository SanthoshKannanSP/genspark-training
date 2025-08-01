using Backend.Interfaces;
using Backend.Models;
using Backend.Models.DTOs;
using Backend.Models.DTOs.Product;
using X.PagedList;
using X.PagedList.Extensions;

namespace Backend.Services;

public class ProductService : IProductService
{
    private readonly IRepository<int, Product> _productRepository;
    public ProductService(IRepository<int, Product> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<PaginatedResponse<ProductResponseDTO>> GetPaginatedProducts(int? page, int? categoryId)
    {
        var pageNumber = page ?? 1;
        var pageSize = 10;
        var products = await _productRepository.GetAllAsync();
        if (categoryId != null)
            products = products.Where(p => p.CategoryId == categoryId);

        var response = products
            .OrderByDescending(p => p.ProductId)
            .Select(p => ProductResponseDTO.MapFrom(p))
            .ToPagedList(pageNumber, pageSize);

        return new PaginatedResponse<ProductResponseDTO>()
        {
            Items = response,
            Page = response.PageNumber,
            TotalPage = response.PageCount
        };
    }

    public async Task<ProductResponseDTO?> GetProduct(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
            return null;
        var response = ProductResponseDTO.MapFrom(product);
        return response;
    }

    public byte[]? GetProductImage(string fileName)
    {
        string imagePath = Path.Combine("Media/Images/Layout", fileName);

        if (!File.Exists(imagePath))
            return null;

        var imageData = File.ReadAllBytes(imagePath);
        return imageData;
    }
}