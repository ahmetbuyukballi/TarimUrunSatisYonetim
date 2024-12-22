using Application.Dtos;

namespace TarimProjesi.Service.IService
{
    public interface IUserService
    {
        public Task<ApiResponse> Register(RegisterDtos models);
       public Task<ApiResponse> Login(LoginDtos models);
       public Task<ApiResponse> GetUser();
    }
}
