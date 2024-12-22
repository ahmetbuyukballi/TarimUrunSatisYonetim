using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abraction
{
    public interface ICommentsService
    {
        Task<ApiResponse> CreatedComments(int userid,int productsId,string description);
        Task<ApiResponse> GetComments(int UserId,int ProductsId);
        Task<ApiResponse> LikeComments(int userId,int ProductsId,int commentsId);
        Task<ApiResponse> DisLikeComments(int userId, int ProductsId, int commentsId);

    }
}
