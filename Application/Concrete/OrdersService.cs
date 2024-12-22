using Application.Abraction;
using Application.Dtos;
using Application.Dtos.OrdersDtos;
using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Domain.Identity;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace Application.Concrete
{
    public class OrdersService : IOrderService
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly ApiResponse _apiResponse;
        private readonly ILogger<OrdersService> _logger;    

        public OrdersService(AppDbContext appDbContext, UserManager<AppUser> userManager,ApiResponse apiResponse,ILogger<OrdersService> logger)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
            _apiResponse = apiResponse;
            _logger = logger;
        }
        
        public async Task<ApiResponse> CreateOrders(int UserId, List<OrderProductDto> models)
        {
            try
            {

                var orderProductTable = new DataTable();
                orderProductTable.Columns.Add("ProductdId", typeof(int));
                orderProductTable.Columns.Add("Quantity", typeof(int));
                foreach (var dto in models)
                {
                    if (dto.Quantity <= 0)
                    {
                        _apiResponse.IsSuccess = false;
                        _apiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                        _apiResponse.ErrorMessages.Add($"Ürün ID {dto.ProductId} için geçersiz miktar:{dto.Quantity}");
                    }
                    orderProductTable.Rows.Add(dto.ProductId, dto.Quantity);
                }
                var orders = (await _appDbContext.Orders
                .FromSqlRaw(
                     "EXECUTE CreateOrder @UserId, @OrderProducts",
                     new SqlParameter("@UserId", UserId),
                        new SqlParameter
                        {
                            ParameterName = "@OrderProducts",
                            SqlDbType = SqlDbType.Structured,
                            TypeName = "dbo.OrderProductType",
                            Value = orderProductTable
                        })
                        .AsNoTracking()
                         .ToListAsync()) // Veriyi çek
                        .AsEnumerable() // Client-side'a al
                        .FirstOrDefault(); // LINQ işlemi burada yapılır
                         
                if (orders == null)
                {
                    _apiResponse.IsSuccess = false;
                    _apiResponse.ErrorMessages.Add("Sipariş oluşturulamadı");
                    _apiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    return _apiResponse;
                }
                _apiResponse.IsSuccess = true;
                _apiResponse.StatusCode = System.Net.HttpStatusCode.OK;
                _apiResponse.Result = orders;
                return _apiResponse;
            }
           
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Stored procedure çalıştırılırken bir hata oluştu.");
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessages.Add(ex.Message);
                return _apiResponse;
               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sipariş oluşturulurken bir hata oluştu.");
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessages.Add($"{ex.Message}");
                return _apiResponse;
            }

        }
      
        public async Task<ApiResponse> DeleteOrders(int UserId, int orderid, int productId)
        {
            var productss = new Products();
            _appDbContext.ChangeTracker.Clear();
            var orders = _appDbContext.Orders.FirstOrDefault(x=>x.Id == orderid);
            if (orders == null)
            {
                _apiResponse.ErrorMessages.Add("Sipariş bulunamadı.");
                _apiResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
                _apiResponse.IsSuccess = false;
                return _apiResponse;
            }

            if (orders.UserId != UserId)
            {
                _apiResponse.ErrorMessages.Add("Kullanıcı bulunamadı.");
                _apiResponse.StatusCode= System.Net.HttpStatusCode.BadRequest;
                _apiResponse.IsSuccess= false;
                return _apiResponse;
            }
            var order = await _appDbContext.Orders
            .Include(o => o.CargoInformation) // CargoInformation ilişkisini yükler
                 .FirstOrDefaultAsync(o => o.Id == orderid);
            
            if (order.CargoInformation.CargoDescription == "Şipariş Hazırlanıyor")
            {
               var products=await _appDbContext.Products.FirstOrDefaultAsync(x=>x.Id==productId);
                var orderItems = await _appDbContext.OrderItems
            .FirstOrDefaultAsync(x => x.OrderId == orders.Id && x.ProductId == productId);
                if (products != null &&orderItems!=null) 
                {
                  
                   
                    if (orderItems.Quantity > 0) 
                    {
                        orderItems.Quantity = orderItems.Quantity - 1;
                        products.Stock = products.Stock + 1;
                        if (orderItems.Quantity == 0)
                        {
                           
                             _appDbContext.OrderItems.Remove(orderItems);
                            
                            
                        }
                        if (await _appDbContext.SaveChangesAsync() > 0)
                        {
                            _apiResponse.IsSuccess = true;
                            _apiResponse.StatusCode = System.Net.HttpStatusCode.OK;
                            _apiResponse.Result = orderItems ;
                            return _apiResponse;
                        }

                    }
                    



                }

            }
            _apiResponse.IsSuccess = false;
            _apiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
            _apiResponse.ErrorMessages.Add("Değiştirebileceğiniz bir sipariş yok.");
            return _apiResponse;
        }

        public async Task<ApiResponse> GetOrderProductId(int userId)
        {
            var order= _appDbContext.Orders.FirstOrDefault(x=>x.UserId == userId);
            if(order == null)
            {
                _apiResponse.ErrorMessages.Add("Kullanıcı yok");
                _apiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _apiResponse.IsSuccess = false;
                return _apiResponse;
            }
            var products= _appDbContext.OrderItems.Where(x=>x.Order.UserId== userId)
                .Select(x=>x.ProductId)
                .ToList();
            if (products.Count > 0) 
            { 
                _apiResponse.IsSuccess=true;
                _apiResponse.StatusCode=System.Net.HttpStatusCode.OK;
                _apiResponse.Result=products ;
                return _apiResponse;
                
            }
            _apiResponse.ErrorMessages.Add("Sipariş bulunamadı");
            _apiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
            _apiResponse.IsSuccess = false;
            return _apiResponse;
        }

    }

      
    
}
