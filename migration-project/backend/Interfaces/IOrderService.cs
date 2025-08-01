using Backend.Models.DTOs;
using Backend.Models.DTOs.Order;
using X.PagedList;

namespace Backend.Interfaces;

public interface IOrderService
{
    Task<PaginatedResponse<OrderResponseDTO>> GetPaginatedOrders(int? page);
    Task<OrderResponseDTO?> GetOrder(int id);
    Task<OrderResponseDTO> CreateOrder(CreateOrderRequestDTO createOrderRequestDTO);
    Task<OrderResponseDTO?> EditOrder(EditOrderRequestDTO editOrderRequestDTO);
    Task<bool> DeleteOrder(int id);
    Task<byte[]> ExportToPdf();
    Task<List<OrderDetailsResponseDTO>> GetOrderDetails(int orderId);
}