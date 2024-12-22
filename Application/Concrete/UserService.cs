using Application.Abraction;
using Application.Dtos;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Domain.Models;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace Application.Concrete
{
    public class UserService : IUserService
    {
        private readonly ApiResponse _apiResponse;
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;
        private IConfiguration _configuration;
        private string secretKey;
        public UserService(UserManager<AppUser> userManager, ApiResponse apiResponse, AppUser appUser,AppDbContext appDbContext,IConfiguration configuration)
        {
            this._userManager = userManager;
            this._apiResponse = apiResponse;
            this._context = appDbContext;
            this._configuration = configuration;
            secretKey = _configuration.GetValue<string>("SecretKey:jwtKey");
        }
        public async Task<ApiResponse> Register(RegisterDtos models)
        {
            var users = await _userManager.FindByEmailAsync(models.Email);

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
                RoleId = 22,

            };
            var result = await _userManager.CreateAsync(appUser, models.Password);

            if (result.Succeeded)
            {
                //await _userManager.AddToRoleAsync(appUser, models.Roles);
                _apiResponse.StatusCode = System.Net.HttpStatusCode.OK;
                _apiResponse.IsSuccess = true;
                return _apiResponse;
            }
            foreach (var errors in result.Errors)
            {
                _apiResponse.ErrorMessages.Add(errors.Description);
            }
            return _apiResponse;
        }
        public async Task<ApiResponse> Login(LoginDtos models)
        {
            AppUser appUser =  _context.AppUsers.FirstOrDefault(x => x.UserName == models.UsernameorEmail || x.Email == models.UsernameorEmail);
            if (appUser != null) {
                bool IsValid = await _userManager.CheckPasswordAsync(appUser, models.Password);
                if (IsValid) {
                    
                    if (appUser == null)
                    {
                        throw new Exception("Kullanıcı bulunamadı!");
                    }

                    var claims = new List<Claim>();

                    if (!string.IsNullOrEmpty(appUser.Id.ToString()))
                    {
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()));
                    }
                    else
                    {
                        throw new Exception("Kullanıcı ID null veya geçersiz!");
                    }

                    if (!string.IsNullOrEmpty(appUser.UserName))
                    {
                        claims.Add(new Claim(ClaimTypes.Name, appUser.UserName));
                    }
                    else
                    {
                        claims.Add(new Claim(ClaimTypes.Name, "Unknown User")); // Default değer atanabilir.
                    }

                    if (!string.IsNullOrEmpty(appUser.Email))
                    {
                        claims.Add(new Claim(ClaimTypes.Email, appUser.Email));
                    }
                    else
                    {
                        claims.Add(new Claim(ClaimTypes.Email, "NoEmail"));
                    }
                    JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                    byte[] key = Encoding.ASCII.GetBytes(secretKey);
                    SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor()
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.NameIdentifier,appUser.Id.ToString()),
                            new Claim(ClaimTypes.Email,appUser.Email),
                            new Claim("Name",appUser.Name),
                        }),
                        Expires = DateTime.UtcNow.AddDays(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                    };
                    SecurityToken token = tokenHandler.CreateToken(securityTokenDescriptor);
                    LoginResponseModels loginResponseModels = new()
                    {
                        Email = appUser.Email,
                        Token = tokenHandler.WriteToken(token),
                    };
                   
                    _apiResponse.Result=loginResponseModels;
                    _apiResponse.IsSuccess=true;
                    _apiResponse.StatusCode = System.Net.HttpStatusCode.OK;
                    return _apiResponse;
                }
                else
                {
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    _apiResponse.ErrorMessages.Add("You entry information is not correct");
                    _apiResponse.IsSuccess = false;
                    return _apiResponse;
                }
            
            }

            _apiResponse.IsSuccess = false;
            _apiResponse.StatusCode = HttpStatusCode.BadRequest;
            _apiResponse.ErrorMessages.Add("Ooopss!! something went wrong");
            return _apiResponse;
        }
        public async Task<ApiResponse> GetUser()
        {
            var users=await _context.AppUsers.ToListAsync();
            if(users.Count>0)
            {
                _apiResponse.IsSuccess = true;
                _apiResponse.StatusCode=HttpStatusCode.OK;
                _apiResponse.Result = users;
                return _apiResponse;
            }
            _apiResponse.IsSuccess=false;
            _apiResponse.StatusCode=HttpStatusCode.BadRequest;
            return _apiResponse;
        }
 

    }
    
}
