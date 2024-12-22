using Application.Abraction;
using Application.Dtos.SalesDtos;
using Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace TarimUrunSatisYonetim.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesControllers : ControllerBase
    {
      
        private readonly ISalesService _salesService;
        public SalesControllers(ISalesService salesService) 
        { 
            _salesService = salesService;
        }
        [HttpPost("DiscountForTheBrands")]
        public async Task<IActionResult> DiscountForTheBrands([FromQuery]int BrandId,[FromBody] CreateSalesDtos models)
        {
            var result = await _salesService.DiscountForTheBrand(BrandId, models);
            if (result != null) 
            { 
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpPost("DiscountForTheCategories")]
        public async Task<IActionResult> DiscountForTheCategories([FromQuery] int CategoryId, [FromBody] CreateSalesDtos models)
        {
            var result = await _salesService.DiscountForTheCategories(CategoryId, models);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpPost("DiscountForTheProducts")]
        public async Task<IActionResult> DiscountForTheProducts([FromQuery] int ProductId, [FromBody] CreateSalesDtos models)
        {
            var result = await _salesService.DiscountForTheProducts(ProductId, models);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
