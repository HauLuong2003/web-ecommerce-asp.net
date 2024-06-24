using Microsoft.AspNetCore.Mvc;
using Web_Ecommerce_Server.Model.Request;

namespace Web_Ecommerce_Server.Service
{
    public interface IUser
    {
        void Register(UserRegisterRequest request);
        Task  Login(LoginRequest loginRequest);
        void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt);
        string CreateRamdomToken();
    }
}
