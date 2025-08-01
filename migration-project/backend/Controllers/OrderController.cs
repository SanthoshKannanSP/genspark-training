using Backend.Interfaces;
using Backend.Models.DTOs.Order;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrderList(int? page)
    {
        var response = await _orderService.GetPaginatedOrders(page);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var response = await _orderService.GetOrder(id);
        if (response == null) return NotFound();
        return Ok(response);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create(CreateOrderRequestDTO dto)
    {
        var response = await _orderService.CreateOrder(dto);
        return Ok(response);
    }

    [HttpPost("Update")]
    public async Task<IActionResult> Update(EditOrderRequestDTO dto)
    {
        var response = await _orderService.EditOrder(dto);
        if (response == null) return NotFound();
        return Ok(response);
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _orderService.DeleteOrder(id);
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpGet("ExportPdf")]
    public async Task<IActionResult> ExportToPdf()
    {

        var pdfBytes = await _orderService.ExportToPdf();
        var fileName = $"OrderListing_{DateTime.UtcNow:yyyyMMdd}.pdf";
        Response.Headers["Content-Disposition"] = $"inline; filename={fileName}";
        return File(pdfBytes, "application/pdf");

    }

    [HttpGet]
    [Route("Details/{id}")]
    public async Task<IActionResult> GetOrderDetails(int id)
    {
        var response = await _orderService.GetOrderDetails(id);
        return Ok(response);
    }
}