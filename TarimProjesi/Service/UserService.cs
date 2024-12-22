using Application.Dtos;
using System.Runtime.CompilerServices;
using TarimProjesi.Service.IService;

namespace TarimProjesi.Service
{
    public class UserService:IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string _BaseUrl;
        private readonly ApiResponse _apiResponse;
        public UserService(HttpClient httpClient, IConfiguration configuration,ApiResponse apiResponse)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _BaseUrl = configuration.GetSection("BaseApiUrl").Value;
            _apiResponse = apiResponse;
        }
        public async Task<ApiResponse> Login(LoginDtos models)
        {
            var result=await _httpClient.GetAsync($"")
        }
        public async Task<ApiResponse> Register(RegisterDtos models)
        {
            return _apiResponse;
        }
        public async Task<ApiResponse> GetUser()
        {
            return _apiResponse;
        }

    }
}
