using Application.Abraction;
using Microsoft.AspNetCore.Mvc;

namespace TarimUrunSatisYonetim.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandsController : Controller
    {
        private readonly IBrandService _brandsService;
        public BrandsController(IBrandService brandsService)
        {
            _brandsService = brandsService;
        }
        [HttpGet("GetBrands")]
        public async Task<IActionResult> GetBrands()
        {
            var result = await _brandsService.GetBrands();
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
