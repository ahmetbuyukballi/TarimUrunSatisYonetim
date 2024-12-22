using Application.Dtos;
using Application.Dtos.CardDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abraction
{
    public interface ICardService
    {
        Task<ApiResponse> CreateCards(string name,CreateDtos models);
        Task<ApiResponse> LoginCards(CreateDtos models);
    }
}
