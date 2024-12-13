using Application.Abraction;
using Application.Concrete;
using Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace TarimUrunSatisYonetim.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController:ControllerBase
    {
        public IUserService _UserService;
        public UserController(IUserService userService)
        {
            _UserService = userService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDtos models)
        {

            var response = await _UserService.Register(models);
            if (response.IsSuccess==true)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
