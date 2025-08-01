using Backend.Interfaces;
using Backend.Models;
using Backend.Models.DTOs;
using Backend.Models.DTOs.Order;
using Backend.MyReport;
using QuestPDF.Fluent;
using X.PagedList;
using X.PagedList.Extensions;

namespace Backend.Services;

public class OrderService : IOrderService
{
    private readonly IRepository<int, Order> _orderRepository;
    private readonly IRepository<int, Product> _productRepository;
    private readonly IRepository<int, OrderDetail> _orderDetailsRepository;

    public OrderService(IRepository<int, Order> orderRepository, IRepository<int, Product> productRepository, IRepository<int, OrderDetail> orderDetailsRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _orderDetailsRepository = orderDetailsRepository;
    }

    public async Task<OrderResponseDTO> CreateOrder(CreateOrderRequestDTO createOrderRequestDTO)
    {
        var order = CreateOrderRequestDTO.MapTo(createOrderRequestDTO);
        order = await _orderRepository.AddAsync(order);
        foreach (var item in createOrderRequestDTO.CartItems)
        {
            var product = await _productRepository.GetByIdAsync(item.ProductId);
            var orderDetail = new OrderDetail
            {
                OrderID = order.OrderID,
                ProductID = item.ProductId,
                Quantity = item.Quantity,
                Price = product.Price
            };

            await _orderDetailsRepository.AddAsync(orderDetail);
        }

        return OrderResponseDTO.MapFrom(order);
    }

    public async Task<bool> DeleteOrder(int id)
    {
        await _orderRepository.DeleteAsync(id);
        return true;
    }
    public async Task<OrderResponseDTO?> EditOrder(EditOrderRequestDTO dto)
    {
        var order = await _orderRepository.GetByIdAsync(dto.OrderID);
        if (order == null) return null;

        order = EditOrderRequestDTO.Combine(dto, order);
        order = await _orderRepository.UpdateAsync(order.OrderID, order);
        return OrderResponseDTO.MapFrom(order);
    }

    public async Task<OrderResponseDTO?> GetOrder(int id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order == null) return null;
        return OrderResponseDTO.MapFrom(order);
    }

    public async Task<PaginatedResponse<OrderResponseDTO>> GetPaginatedOrders(int? page)
    {
        int pageNumber = page ?? 1;
        int pageSize = 5;

        var orders = await _orderRepository.GetAllAsync();
        var response = orders.OrderByDescending(o => o.OrderID)
                           .Select(OrderResponseDTO.MapFrom)
                           .ToPagedList(pageNumber, pageSize);
        return new PaginatedResponse<OrderResponseDTO>
        {
            Items = response,
            Page = response.PageNumber,
            TotalPage = response.PageCount
        };
    }

    public async Task<List<OrderDetailsResponseDTO>> GetOrderDetails(int orderId)
    {
        var orders = await _orderDetailsRepository.GetAllAsync();
        var response = orders.Where(o => o.OrderID == orderId).Select(o => new OrderDetailsResponseDTO()
        {
            Price = o.Price,
            ProductID = o.ProductID,
            ProductName = o.Product.ProductName,
            Quantity = o.Quantity
        }).ToList();
        return response;
    }

    public async Task<byte[]> ExportToPdf()
    {
        var orders = await _orderRepository.GetAllAsync();
        var report = new OrderListing(orders.ToList());
        var pdfBytes = report.GeneratePdf();
        return pdfBytes.ToArray();
    }
}
