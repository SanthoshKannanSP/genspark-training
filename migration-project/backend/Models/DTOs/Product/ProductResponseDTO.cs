namespace Backend.Models.DTOs.Product;

public class ProductResponseDTO
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public double? Price { get; set; }
    public int? UserId { get; set; }
    public int? CategoryId { get; set; }
    public int? ColorId { get; set; }
    public int? ModelId { get; set; }
    public int? StorageId { get; set; }
    public DateTime? SellStartDate { get; set; }
    public DateTime? SellEndDate { get; set; }
    public int? IsNew { get; set; }

    public static ProductResponseDTO MapFrom(Models.Product product)
    {
        return new ProductResponseDTO
        {
            ProductId = product.ProductId,
            ProductName = product.ProductName,
            Image = product.Image,
            Price = product.Price,
            UserId = product.UserId,
            CategoryId = product.CategoryId,
            ColorId = product.ColorId,
            ModelId = product.ModelId,
            StorageId = product.StorageId,
            SellStartDate = product.SellStartDate,
            SellEndDate = product.SellEndDate,
            IsNew = product.IsNew
        };
    }
}