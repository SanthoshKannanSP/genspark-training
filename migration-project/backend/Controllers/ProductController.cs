using Backend.Interfaces;
using DocumentFormat.OpenXml.Vml;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts(int? page, int? category)
    {
        var products = await _productService.GetPaginatedProducts(page, category);
        return Ok(products);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var product = await _productService.GetProduct(id);
        if (product == null) return NotFound();
        return Ok(product);
    }

    [HttpGet("Image/{fileName}")]
    public IActionResult GetImage(string fileName)
    {
        var image = _productService.GetProductImage(fileName);

        if (image == null)
            return NotFound();

        return File(image, "image/jpeg");
    }
}