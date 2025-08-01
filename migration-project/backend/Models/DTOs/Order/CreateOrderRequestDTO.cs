namespace Backend.Models.DTOs.Order;

public class CreateOrderRequestDTO
{
    public DateTime? OrderDate { get; set; }
    public string CustomerName { get; set; }
    public string CustomerPhone { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAddress { get; set; }
    public List<CartItemDTO> CartItems { get; set; }

    public class CartItemDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }


    public static Models.Order MapTo(CreateOrderRequestDTO createOrderRequestDTO)
    {
        return new Models.Order
        {
            OrderName = "",
            OrderDate = DateTime.UtcNow,
            PaymentType = "Cash",
            Status = "Processing",
            CustomerName = createOrderRequestDTO.CustomerName,
            CustomerPhone = createOrderRequestDTO.CustomerPhone,
            CustomerEmail = createOrderRequestDTO.CustomerEmail,
            CustomerAddress = createOrderRequestDTO.CustomerAddress
        };
    }
}