using Microsoft.AspNetCore.Mvc;
using Web_Ecommerce_Server.Model.Request;
using Web_Ecommerce_Server.Response;

namespace Web_Ecommerce_Server.Service
{
    public interface IUserAccount
    {
        Task<ServiceResponse> Register(UserRegisterRequest request);
        Task<ServiceResponse> Login(LoginRequest request);
        Task<ServiceResponse> ForgotPassword(string email);
        Task<ServiceResponse> ResetPassword(ResetPassword resetPassword);
        Task<ServiceResponse> Verify(string token);
    }
}
