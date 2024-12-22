using Application.Abraction;
using Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Formats.Asn1;

namespace TarimUrunSatisYonetim.Controllers
{
    public class ProductsControllers : Controller
    {
        private readonly IProductsService _productsService;
        public ProductsControllers(IProductsService productsService)
        {
            _productsService = productsService;
        }
        [HttpPost("Create")]
        public async Task<IActionResult> CreateProducts([FromBody] CreateProductsdTOS models)
        {
            var response = await _productsService.CreateProducts(models);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteProducts([FromQuery] int id)
        {
            var response = await _productsService.DeleteProducts(id);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetProducts()
        {
            var response = await _productsService.GetProducts();
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpPost("Update")]
        public async Task<IActionResult> UpdateProducts([FromQuery] int id, [FromBody] UpdateProductsDtos models)
        {
            var response = await _productsService.UpdateProducts(id, models);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetProductsById([FromQuery] int id)
        {
            var response = await _productsService.GetProductsById(id);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
      
    }
}
