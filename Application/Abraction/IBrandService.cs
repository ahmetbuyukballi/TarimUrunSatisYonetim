using Application.Dtos;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abraction
{
    public interface IBrandService
    {
        Task<ApiResponse> GetBrands();
    }
}
