using Application;
using Application.Abraction;
using Application.Concrete;
using Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace TarimUrunSatisYonetim.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        public IUserService _UserService;
        public IConfiguration _configuration;

        public UserController(IUserService userService, IConfiguration configuration)
        {
            _UserService = userService;
            _configuration = configuration;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDtos models)
        {

            var response = await _UserService.Register(models);
            if (response.IsSuccess == true)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDtos models)
        {
            var result = await _UserService.Login(models);
            if (result.IsSuccess == true)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _UserService.GetUser();
            if (result.IsSuccess == true) 
            {
                return Ok(result);
            }
            return BadRequest();
        }

    }
}
