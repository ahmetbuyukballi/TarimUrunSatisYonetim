using Application.Abraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TarimUrunSatisYonetim.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsControllers : ControllerBase
    {
        private readonly ICommentsService _commentsService;
        private readonly ILogger<CommentsControllers> _logger;
        public CommentsControllers(ICommentsService commentsService,ILogger<CommentsControllers> logger) 
        { 
            _commentsService = commentsService;
            _logger = logger;
        }
        [Authorize]
        [HttpPost("CreateComments")]
       public async Task<IActionResult> CreateComments(int productId, string description)
        {
            var userid= User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userid))
            {
                _logger.LogWarning("Kullanıcı kimliği alınamadı. Mevcut claim'ler: {@Claims}", User.Claims);
                return Unauthorized();
            }
            if (!int.TryParse(userid, out int Userid))
            {
                _logger.LogWarning("Kullanıcı kimliği alınamadı veya geçersiz. Mevcut claim: {@Claims}", User.Claims);
                return Unauthorized();
            }
            var result = await _commentsService.CreatedComments(Userid, productId, description);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [Authorize]
        [HttpGet("CommentsGet")]
        public async Task<IActionResult> GetComments(int productsId)
        {
            var userid=User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userid))
            {
                _logger.LogWarning("Kullanıcı kimliği alınamadı. Mevcut claim'ler: {@Claims}", User.Claims);
                return Unauthorized();
            }
            if (!int.TryParse(userid, out int Userid))
            {
                _logger.LogWarning("Kullanıcı kimliği alınamadı veya geçersiz. Mevcut claim: {@Claims}", User.Claims);
                return Unauthorized();
            }
            var result=await _commentsService.GetComments(Userid, productsId);
            return Ok(result);
        }
        [Authorize]
        [HttpPut("LikeComments")]
        public async Task<IActionResult> LikeComments(int productsId,int commentsId)
        {
            var userid = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userid))
            {
                _logger.LogWarning("Kullanıcı kimliği alınamadı. Mevcut claim'ler: {@Claims}", User.Claims);
                return Unauthorized();
            }
            if (!int.TryParse(userid, out int Userid))
            {
                _logger.LogWarning("Kullanıcı kimliği alınamadı veya geçersiz. Mevcut claim: {@Claims}", User.Claims);
                return Unauthorized();
            }
            var result=await _commentsService.LikeComments(Userid, productsId, commentsId);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();

        }
        [Authorize]
        [HttpPut("DisLikeComments")]
        public async Task<IActionResult> DisLikeComments(int productsId, int commentsId)
        {
            var userid = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userid))
            {
                _logger.LogWarning("Kullanıcı kimliği alınamadı. Mevcut claim'ler: {@Claims}", User.Claims);
                return Unauthorized();
            }
            if (!int.TryParse(userid, out int Userid))
            {
                _logger.LogWarning("Kullanıcı kimliği alınamadı veya geçersiz. Mevcut claim: {@Claims}", User.Claims);
                return Unauthorized();
            }
            var result =await _commentsService.DisLikeComments(Userid, productsId, commentsId);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();

        }
    }
}
