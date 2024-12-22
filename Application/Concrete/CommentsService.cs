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
    public class CommentsService:ICommentsService
    {   private readonly ApiResponse _apiResponse;
        private readonly AppDbContext _appdbContext;
        public CommentsService(ApiResponse apiResponsei,AppDbContext appDbContext) 
        { 
            _apiResponse = apiResponsei;
            _appdbContext = appDbContext;
        }
       public async Task<ApiResponse> CreatedComments(int userid,int productsId,string description)
        {
           var products=_appdbContext.Products.FirstOrDefault(x=>x.Id == productsId);
            var Userid =  _appdbContext.Users.FirstOrDefault(x => x.Id == userid);
            if (Userid != null)
            {
                var comments = new Comments()
                {
                    Description = description,
                    UserId = userid,
                    ProductsId= productsId,
                };
                _appdbContext.comments.Add(comments);
                if (_appdbContext.SaveChanges() > 0)
                {
                    _apiResponse.IsSuccess = true;
                    _apiResponse.StatusCode = System.Net.HttpStatusCode.OK;
                    _apiResponse.Result = comments;
                    return _apiResponse;
                }
                _apiResponse.IsSuccess = false;
                _apiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _apiResponse.ErrorMessages.Add("Yorum Eklenemedi");
                return _apiResponse;
            }
            _apiResponse.IsSuccess = true;
            _apiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
            _apiResponse.ErrorMessages.Add("Kullanıcı bulunamadı");
            return _apiResponse;

        }
        public async Task<ApiResponse> GetComments(int UserId, int ProductsId)
        {
            var products=_appdbContext.Products.FirstOrDefault(x=>x.Id == ProductsId);
            var Userid = _appdbContext.Users.FirstOrDefault(x => x.Id == UserId);
            var comments = _appdbContext.comments.Where(x => x.ProductsId == ProductsId);
            if (Userid != null)
            {
               var result=await comments.ToListAsync();
                if(result.Count > 0)
                {
                    _apiResponse.IsSuccess = true;
                    _apiResponse.Result = result;
                    _apiResponse.StatusCode = System.Net.HttpStatusCode.OK;
                    return _apiResponse;
                }
                
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessages.Add("Üründe yorum mevcut değil");
                _apiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return _apiResponse;
            }
            _apiResponse.IsSuccess = false;
            _apiResponse.ErrorMessages.Add("Kullanıcı bulunmadı");
            _apiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
            return _apiResponse;
        }
        public async Task<ApiResponse> LikeComments(int userId, int productId, int commentsId)
        {
            // Ürün kontrolü
            var product = await _appdbContext.Products.FirstOrDefaultAsync(x => x.Id == productId);
            if (product == null)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    ErrorMessages = new List<string> { "Ürün bulunamadı" }
                };
            }

            // Yorum kontrolü
            var comment = await _appdbContext.comments.FirstOrDefaultAsync(x => x.Id == commentsId);
            if (comment == null)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    ErrorMessages = new List<string> { "Yorum bulunamadı" }
                };
            }

            // Puan artırma işlemi
            comment.Scoring += 1;
            _appdbContext.comments.Update(comment);

            await _appdbContext.SaveChangesAsync();

            return new ApiResponse
            {
                IsSuccess = true,
                StatusCode = System.Net.HttpStatusCode.OK,
                Result = new
                {
                    CommentId = comment.Id,
                    NewScore = comment.Scoring
                }
            };
        }

        public async Task<ApiResponse> DisLikeComments(int userId, int ProductsId, int commentsId)
        {
            var product = await _appdbContext.Products.FirstOrDefaultAsync(x => x.Id == ProductsId);
            if (product == null)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    ErrorMessages = new List<string> { "Ürün bulunamadı" }
                };
            }

            // Yorum kontrolü
            var comment = await _appdbContext.comments.FirstOrDefaultAsync(x => x.Id == commentsId);
            if (comment == null)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    ErrorMessages = new List<string> { "Yorum bulunamadı" }
                };
            }

            // Puan artırma işlemi
            comment.Scoring -= 1;
            _appdbContext.comments.Update(comment);

            await _appdbContext.SaveChangesAsync();

            return new ApiResponse
            {
                IsSuccess = true,
                StatusCode = System.Net.HttpStatusCode.OK,
                Result = new
                {
                    CommentId = comment.Id,
                    NewScore = comment.Scoring
                }
            };
        }
    }
}
