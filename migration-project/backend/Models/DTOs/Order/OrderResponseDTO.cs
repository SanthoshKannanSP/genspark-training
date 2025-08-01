namespace Backend.Models.DTOs.Order;

public class OrderResponseDTO
{
    public int OrderID { get; set; }
    public string OrderName { get; set; }
    public DateTime? OrderDate { get; set; }
    public string PaymentType { get; set; }
    public string Status { get; set; }
    public string CustomerName { get; set; }
    public string CustomerPhone { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAddress { get; set; }

    public static OrderResponseDTO MapFrom(Models.Order order)
    {
        return new OrderResponseDTO
        {
            OrderID = order.OrderID,
            OrderName = order.OrderName,
            OrderDate = order.OrderDate,
            PaymentType = order.PaymentType,
            Status = order.Status,
            CustomerName = order.CustomerName,
            CustomerPhone = order.CustomerPhone,
            CustomerEmail = order.CustomerEmail,
            CustomerAddress = order.CustomerAddress
        };
    }
}