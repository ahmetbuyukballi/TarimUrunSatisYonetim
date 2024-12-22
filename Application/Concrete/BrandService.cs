using Application.Abraction;
using Application.Dtos;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Concrete
{
    public class BrandService : IBrandService
    {
        private readonly AppDbContext _appDbContext;
        private readonly ApiResponse _apiResponse;
        public BrandService(AppDbContext appDbContext, ApiResponse apiResponse)
        {
            _appDbContext = appDbContext;
            _apiResponse = apiResponse;
        }
        public async Task<ApiResponse> GetBrands()
        {
            var result = _appDbContext.Brands.ToList();
            if (result == null)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _apiResponse.ErrorMessages.Add("Markalar mevcut değil");
                return _apiResponse;
            }
            _apiResponse.IsSuccess = true;
            _apiResponse.StatusCode = System.Net.HttpStatusCode.OK;
            _apiResponse.Result = result;
            return _apiResponse;
        }
    }
}