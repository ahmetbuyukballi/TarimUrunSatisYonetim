using Application.Dtos;
using Application.Dtos.SalesDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abraction
{
    public interface ISalesService
    {
          Task<ApiResponse> DiscountForTheBrand(int BrandId,CreateSalesDtos models); 
          Task<ApiResponse> DiscountForTheCategories(int CategoryId,CreateSalesDtos models);
          Task<ApiResponse> DiscountForTheProducts(int ProductId,CreateSalesDtos models);
    }
}
