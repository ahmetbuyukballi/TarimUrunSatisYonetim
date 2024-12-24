using Application.Abraction;
using Application.Dtos;
using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Concrete
{
    public class ProductsService : IProductsService
    { private readonly AppDbContext _appDbContext;
        private readonly ApiResponse _apiResponse;
        public ProductsService(AppDbContext appDbContext,ApiResponse apiResponse) 
        { 
            _appDbContext = appDbContext;
            _apiResponse = apiResponse;
        }
        
        public async Task<ApiResponse> CreateProducts(CreateProductsdTOS models)
        {
            if (models != null)
            {
                Products products = new()
                {
                    Name = models.Name,
                    Price = models.Price,
                    MeasurementId = models.MeasurementId,
                    Stock = models.Stock,
                    CategoriesId = models.CategoriesId,
                    BrandId = models.BrandId,
                    UserId=15592
                };
                await _appDbContext.Products.AddAsync(products);
                if(await _appDbContext.SaveChangesAsync() > 0) 
                {
                    _apiResponse.IsSuccess = true;
                    _apiResponse.StatusCode = System.Net.HttpStatusCode.OK;
                    _apiResponse.Result = products;
                    return _apiResponse;

                }
                _apiResponse.IsSuccess = false;
                _apiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _apiResponse.ErrorMessages.Add("Add operation failed");
                return _apiResponse;
            }
            _apiResponse.IsSuccess=false;
            _apiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
            _apiResponse.ErrorMessages.Add("There are empity spaces");
            return _apiResponse;
        }

        public async Task<ApiResponse> DeleteProducts(int id)
        {
            var products= _appDbContext.Products.FirstOrDefault(x=>x.Id==id);
            if (products == null)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.StatusCode= System.Net.HttpStatusCode.BadRequest;
                _apiResponse.ErrorMessages.Add("Products not find");
                return _apiResponse;
            }
             _appDbContext.Products.Remove(products);
            if (_appDbContext.SaveChanges() > 0) 
            {
                _apiResponse.IsSuccess = true;
                _apiResponse.StatusCode = System.Net.HttpStatusCode.OK;
                return _apiResponse;
            }

            _apiResponse.IsSuccess = false;
            _apiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
            _apiResponse.ErrorMessages.Add("There was an error deleting");
            return _apiResponse;
        }

        public async Task<ApiResponse> GetProducts()
        {
            var prodcts = await _appDbContext.Products.ToListAsync();
            if (prodcts == null)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.StatusCode=System.Net.HttpStatusCode.BadRequest;
                _apiResponse.ErrorMessages.Add("asd");
                return _apiResponse;
            }
            _apiResponse.IsSuccess= true;
            _apiResponse.StatusCode=System.Net.HttpStatusCode.OK;
            _apiResponse.Result=prodcts;
            return _apiResponse;
           
        }

        public async Task<ApiResponse> GetProductsById(int id)
        {
            var products=await _appDbContext.Products.FirstOrDefaultAsync(x=>x.Id == id);
            if(products == null)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.StatusCode= System.Net.HttpStatusCode.BadRequest;
                _apiResponse.ErrorMessages.Add("Product not found");
                return _apiResponse;
            }
            _apiResponse.IsSuccess= true;
            _apiResponse.StatusCode = System.Net.HttpStatusCode.OK;
            _apiResponse.Result = products;
            return _apiResponse;

        }

        public async Task<ApiResponse> UpdateProducts(int id,UpdateProductsDtos models)
        {
            var products=await _appDbContext.Products.FirstOrDefaultAsync(x=>x.Id == id);
            if (products != null) 
            {
                products.Name = models.Name;
                products.Price = models.Price;
                products.MeasurementId = models.MeasurementId;
                products.Stock = models.Stock;
                products.CategoriesId = models.CategoriesId;
                products.BrandId = models.BrandId;
                products.UserId = 15592;
                _appDbContext.Products.Update(products);
                if (_appDbContext.SaveChanges() > 0)
                {
                    _apiResponse.IsSuccess = true;
                    _apiResponse.StatusCode=System.Net.HttpStatusCode.OK;
                    _apiResponse.Result= products;
                    return _apiResponse;
                }
                _apiResponse.IsSuccess = false;
                _apiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _apiResponse.ErrorMessages.Add("Could not add user")
;                return _apiResponse;
            }
            _apiResponse.IsSuccess= false;
            _apiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
            return _apiResponse;
        }
    }
}
