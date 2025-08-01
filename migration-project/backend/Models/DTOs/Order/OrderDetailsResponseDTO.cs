namespace Backend.Models.DTOs.Order;

public class OrderDetailsResponseDTO
{
    public int ProductID { get; set; }
    public string ProductName { get; set; }
    public double? Price { get; set; }
    public int? Quantity { get; set; }

}