using Application.Abraction;
using Microsoft.AspNetCore.Mvc;

namespace TarimUrunSatisYonetim.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryControllers : ControllerBase
    {
        private readonly ICategoryServices _categoryServices;
        public CategoryControllers(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }
        [HttpGet("GetCategory")]
        public async Task<IActionResult> GetCategory()
        {
            var result = await _categoryServices.GetCategories();
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }
    }
}
