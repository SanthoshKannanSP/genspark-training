namespace Backend.Models.DTOs.Order;

public class EditOrderRequestDTO
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

    public static Models.Order Combine(EditOrderRequestDTO dto, Models.Order order)
    {
        order.OrderName = dto.OrderName;
        order.OrderDate = dto.OrderDate;
        order.PaymentType = dto.PaymentType;
        order.Status = dto.Status;
        order.CustomerName = dto.CustomerName;
        order.CustomerPhone = dto.CustomerPhone;
        order.CustomerEmail = dto.CustomerEmail;
        order.CustomerAddress = dto.CustomerAddress;
        return order;
    }
}