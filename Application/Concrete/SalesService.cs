using Application.Abraction;
using Application.Dtos;
using Application.Dtos.SalesDtos;
using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Concrete
{
    public class SalesService : ISalesService
    { private readonly AppDbContext _appDbContext;
        private readonly ApiResponse _apiResponse;
        public SalesService(AppDbContext appDbContext, ApiResponse apiResponse)
        {
            _appDbContext = appDbContext;
            _apiResponse = apiResponse;
        }
        public async Task<ApiResponse> DiscountForTheBrand(int BrandId, CreateSalesDtos models)
        {
            var brands = _appDbContext.Brands.FirstOrDefault(x => x.Id == BrandId);
            if (brands != null)
            {
                var sales = new Sales()
                {
                    Name = models.Name,
                    Rate = models.Rate,
                    StartDate = models.StartDate,
                    EndDate = models.EndDate,
                };
                var products = _appDbContext.Products
                .Where(x => x.BrandId == BrandId);
                if (products.Count() > 0)
                {
                    foreach (var product in products)
                    {
                        product.Price = product.Price * (100 - models.Rate) / 100;
                        _appDbContext.Products.Update(product);

                    }
                    if (_appDbContext.SaveChanges() > 0)
                    {
                        _apiResponse.IsSuccess = true;
                        _apiResponse.StatusCode = System.Net.HttpStatusCode.OK;
                        _apiResponse.Result = products;
                        return _apiResponse;
                    }

                }

            }
            _apiResponse.IsSuccess = false;
            _apiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
            _apiResponse.ErrorMessages.Add("Marka bulunamadı.");
            return _apiResponse;
        }
        public async Task<ApiResponse> DiscountForTheCategories(int categoryId, CreateSalesDtos models)
        {
            var categories = _appDbContext.Categories.FirstOrDefault(x => x.Id == categoryId);
            if (categories != null)
            {
                var sales = new Sales()
                {
                    Name = models.Name,
                    Rate = models.Rate,
                    StartDate = models.StartDate,
                    EndDate = models.EndDate,
                };
                var products = _appDbContext.Products.Where(x => x.CategoriesId == categoryId);
                if (products.Count() > 0)
                {
                    foreach (var product in products)
                    {
                        product.Price = product.Price * (100 - models.Rate) / 100;
                        _appDbContext.Products.Update(product);
                    }
                    if (_appDbContext.SaveChanges() > 0)
                    {
                        _apiResponse.IsSuccess = true;
                        _apiResponse.StatusCode = System.Net.HttpStatusCode.OK;
                        _apiResponse.Result = products;
                        return _apiResponse;
                    }
                }
            }
            _apiResponse.IsSuccess = false;
            _apiResponse.ErrorMessages.Add("Bu kategori mevcut değil");
            _apiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
            return _apiResponse;
        }

        public async Task<ApiResponse> DiscountForTheProducts(int ProductId, CreateSalesDtos models)
        {
            var products = _appDbContext.Products.Where(x => x.Id == ProductId);
            if (products != null)
            {
                var sales = new Sales()
                {
                    Name = models.Name,
                    Rate = models.Rate,
                    StartDate = models.StartDate,
                    EndDate = models.EndDate,
                };
                foreach (var product in products)
                {
                    product.Price = product.Price * (100 - models.Rate) / 100;
                    _appDbContext.Products.Update(product);
                }
                if (_appDbContext.SaveChanges() > 0)
                {
                    _apiResponse.IsSuccess = true;
                    _apiResponse.StatusCode = System.Net.HttpStatusCode.OK;
                    _apiResponse.Result = products;
                    return _apiResponse;

                }
            }
            _apiResponse.IsSuccess = false;
            _apiResponse.ErrorMessages.Add("Bu ürün mevcut değil");
            _apiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
            return _apiResponse;
        }
        
    }
}
