using Application.Abraction;
using Application.Dtos;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Concrete
{
    public class UserService:IUserService
    {
        private readonly ApiResponse _apiResponse;
        private readonly UserManager<AppUser> _userManager;
        
        public UserService(UserManager<AppUser> userManager,ApiResponse apiResponse,AppUser appUser)
        {
            this._userManager = userManager;
            this._apiResponse = apiResponse;
        }
        public async Task<ApiResponse> Register(RegisterDtos models)
        {
            var users=await _userManager.FindByEmailAsync(models.Email);
           
            if (users != null)
            {
                _apiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _apiResponse.ErrorMessages.Add("User already exits");
                _apiResponse.IsSuccess = false;
                return _apiResponse;
            }
            AppUser appUser = new()
            {
                Name = models.Name,
                Surname = models.Surname,
                Email = models.Email,
                Gender = models.Gender,
                HomeAdress = models.HomeAdress,
                PhoneNumber = models.PhoneNumber,
                UserName = models.UserName,
                RoleId=2,
                
            };
            var result = await _userManager.CreateAsync(appUser, models.Password);

            if (result.Succeeded)
            {
                //await _userManager.AddToRoleAsync(appUser, models.Roles);
                _apiResponse.StatusCode = System.Net.HttpStatusCode.OK;
                _apiResponse.IsSuccess=true;
                return _apiResponse;
            }
            foreach(var errors in result.Errors)
            {
                _apiResponse.ErrorMessages.Add(errors.Description);
            }
            return _apiResponse;
        }
        public async Task<ApiResponse> Login(LoginDtos models)
        {
            return _apiResponse;
        }
    }
}
