using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abraction
{
    public interface IProductsService
    {
        Task<ApiResponse> GetProducts();
        Task<ApiResponse> CreateProducts(CreateProductsdTOS models);
        Task<ApiResponse> UpdateProducts(int id,UpdateProductsDtos models);
        Task<ApiResponse> DeleteProducts(int id);
        Task<ApiResponse> GetProductsById(int id);

    }
}
