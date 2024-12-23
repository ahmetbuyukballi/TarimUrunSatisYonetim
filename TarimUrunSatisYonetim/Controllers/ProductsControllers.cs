using Application.Abraction;
using Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Formats.Asn1;
using System.Security.Claims;

namespace TarimUrunSatisYonetim.Controllers
{
    public class ProductsControllers : Controller
    {
        private readonly IProductsService _productsService;
        private readonly ILogger<ProductsControllers> _logger;
        public ProductsControllers(IProductsService productsService,ILogger<ProductsControllers> logger)
        {
            _productsService = productsService;
            _logger = logger;
        }
        [Authorize]
        [HttpPost("Create")]
        public async Task<IActionResult> CreateProducts([FromBody] CreateProductsdTOS models)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            if (userEmail == null || userEmail != "admin@example.com")
            {
                return Forbid();
            }

            var response = await _productsService.CreateProducts(models);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
               
            return BadRequest();
           
        }
        [Authorize]
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteProducts([FromQuery] int id)
        {

            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            if (userEmail == null || userEmail != "admin@example.com")
            {
                return Forbid();
            }

            var response = await _productsService.DeleteProducts(id);
                if (response.IsSuccess==true)
                {
                    return Ok(response);
                }
                return BadRequest();

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
        [Authorize]
        [HttpPost("Update")]
        public async Task<IActionResult> UpdateProducts([FromQuery] int id, [FromBody] UpdateProductsDtos models)
        {

            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            if (userEmail == null || userEmail != "admin@example.com")
            {
                return Forbid();
            }

            var response = await _productsService.UpdateProducts(id, models);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return BadRequest();
            

        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetProductsById([FromQuery] int id)
        {
            var response = await _productsService.GetProductsById(id);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest();
        }
      
    }
}
