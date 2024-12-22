using Application.Abraction;
using Application.Dtos;
using Application.Dtos.CardDtos;
using Domain.Entites;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Concrete
{
    public class CardService:ICardService
    {   
        private readonly ApiResponse _apiResponse;
        private readonly AppDbContext _appDbContext;
        public CardService(ApiResponse apiResponse,AppDbContext appDbContext) 
        { 
            _apiResponse = apiResponse;
            
            _appDbContext = appDbContext;
        }
        public async Task<ApiResponse> CreateCards(string name,CreateDtos models)
        {
            var UserName= _appDbContext.CardInformation.FirstOrDefault(x=>x.CardHolderName==name);
            if (UserName != null)
            {
                var result = new CardInformation()
                {
                    CardHolderName = UserName.CardHolderName,
                    CardNumber = UserName.CardNumber,
                    ExpiryDate = UserName.ExpiryDate,
                    CVV = UserName.CVV,

                };
                _apiResponse.StatusCode = System.Net.HttpStatusCode.OK;
                _apiResponse.IsSuccess = true;
                _apiResponse.Result= result;
                return _apiResponse;
            }
            var card = new CardInformation()
            {
                CardHolderName = models.CardHolderName,
                CardNumber = models.CardNumber,
                ExpiryDate = models.ExpiryDate,
                CVV = models.CVV,
            };
            if (card == null)
            {
                _apiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessages.Add("Kart bilgileri girilemedi");
                return _apiResponse;
            }
            _appDbContext.CardInformation.Add(card);
            _appDbContext.SaveChanges();
            _apiResponse.StatusCode = System.Net.HttpStatusCode.OK;
            _apiResponse.IsSuccess = true;
            _apiResponse.Result = card;
            return _apiResponse;
        }
     
        public async Task<ApiResponse> LoginCards(CreateDtos models)
        {
            var result= _appDbContext.CardInformation.FirstOrDefault(x=>x.CardNumber==models.CardNumber);
            if(result == null)
            {
                _apiResponse.IsSuccess= false;
                _apiResponse.ErrorMessages.Add("Kart bilgileri bulunamadı");
                _apiResponse.StatusCode= System.Net.HttpStatusCode.BadRequest;
                return _apiResponse;
            }
            _apiResponse.IsSuccess= true;
            _apiResponse.StatusCode=System.Net.HttpStatusCode.OK;
            _apiResponse.Result=result;
            return _apiResponse;
        }
    
    }
   
}
