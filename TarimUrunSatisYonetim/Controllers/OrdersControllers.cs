using Application.Abraction;
using Application.Dtos;
using Application.Dtos.OrdersDtos;
using Azure;
using Domain.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System.Security.Claims;

namespace TarimUrunSatisYonetim.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class OrdersControllers : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly IOrderService _orderService;
        private readonly ILogger<OrdersControllers> _logger;
        public OrdersControllers(HttpClient httpClient, IOrderService orderService, ILogger<OrdersControllers> logger)
        {
            _httpClient = httpClient;
            _orderService = orderService;
            _logger = logger;
        }
        [Authorize]
        [HttpPost("Create")]
        public async Task<IActionResult> CreateOrders([FromBody] List<OrderProductDto> models)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogWarning("Kullanıcı kimliği alınamadı. Mevcut claim'ler: {@Claims}", User.Claims);
                return Unauthorized("Kullanıcı kimliği alınamadı.");
            }

            if (!int.TryParse(userId, out int userid))
            {
                return Unauthorized("Kullanıcı kimliği alınamadı.");
            }
            var result = await _orderService.CreateOrders(userid, models);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();


        }
        [Authorize]
        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteOrders([FromQuery] int ordersid, [FromQuery] int productId)
        {
            var useridClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(useridClaim))
            {
                _logger.LogWarning("Kullanıcı kimliği alınamadı. Mevcut claim'ler: {@Claims}", User.Claims);
                return Unauthorized();
            }
            if (!int.TryParse(useridClaim, out int userid))
            {
                _logger.LogWarning("Kullanıcı kimliği alınamadı veya geçersiz. Mevcut claim: {@Claims}", User.Claims);
                return Unauthorized();
            }

            var result = await _orderService.DeleteOrders(userid, ordersid, productId);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }
        [HttpGet("test-token")]
        public async Task<IActionResult> TestToken()
        {
            var authHeader = Request.Headers["Authorization"].ToString();

            if (string.IsNullOrEmpty(authHeader))
            {
                return Unauthorized("Authorization header gönderilmedi!");
            }

            _logger.LogInformation($"Authorization Header: {authHeader}");

            return Ok("Token başarıyla alındı!");
        }
        [Authorize]
        [HttpGet("OrderProducts-Get")]
        public async Task<IActionResult> OrderProductsGet() 
        {
            var UserId=User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(UserId))
            {
                _logger.LogWarning("Kullanıcı kimliği alınamadı. Mevcut claim'ler: {@Claims}", User.Claims);
                return Unauthorized();
            }
            if (!int.TryParse(UserId, out int userid))
            {
                _logger.LogWarning("Kullanıcı kimliği alınamadı veya geçersiz. Mevcut claim: {@Claims}", User.Claims);
                return Unauthorized();
            }
            var result = await _orderService.GetOrderProductId(userid);
            if (result != null) 
            { 
                return Ok(result);
            }
            return BadRequest();
        
        }
    }
}
