using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abraction
{
    public interface ICategoryServices
    {
        Task<ApiResponse> GetCategories();
    }
}
