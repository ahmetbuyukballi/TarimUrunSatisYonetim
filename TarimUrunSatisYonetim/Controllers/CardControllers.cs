using Application.Abraction;
using Application.Dtos;
using Application.Dtos.CardDtos;
using Microsoft.AspNetCore.Mvc;

namespace TarimUrunSatisYonetim.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardControllers : ControllerBase
    {
        private readonly ICardService _cardService;
       public CardControllers(ICardService cardService) 
        { 
            _cardService = cardService;
        }
        [HttpPost("CreateCards")]
       public async Task<IActionResult> CreateCards(string name, CreateDtos models)
        {
            var result=await _cardService.CreateCards(name, models);
            if (result != null) 
            { 
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpPost("LoginCards")]
        public async Task<IActionResult> GetCards(CreateDtos models)
        {
            var result = await _cardService.LoginCards(models);
            if (result.IsSuccess==false)
            {
                return BadRequest(result);
            }
            return Ok();
        }
    }
}
