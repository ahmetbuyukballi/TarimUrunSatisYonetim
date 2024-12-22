using Application.Dtos;
using Application.Dtos.OrdersDtos;
using Domain.Entites;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abraction
{
    public interface IOrderService
    {
        
       // Task<ApiResponse> UpdateOrders(int id,OrdersUpdateDtos models);
        Task<ApiResponse> DeleteOrders(int UserId,int orderid,int productId);
        Task<ApiResponse> CreateOrders(int UserId,List<OrderProductDto> models);
        Task<ApiResponse> GetOrderProductId(int userId);
    }
}
