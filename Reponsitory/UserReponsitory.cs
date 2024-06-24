using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Web_Ecommerce_Server.Model.Entity;
using Web_Ecommerce_Server.Model.Request;
using Web_Ecommerce_Server.Service;

namespace Web_Ecommerce_Server.Reponsitory
{
    public class UserReponsitory : IUser
    {
        private readonly WebEcommerceContext webEcommerceContext;
        private IUserService userService;
        public UserReponsitory(WebEcommerceContext webEcommerceContext, IUserService userService) 
        {
            this.webEcommerceContext = webEcommerceContext;
           this.userService = userService;
        }
        // dang nhap
        public async Task Login(LoginRequest loginRequest)
        {
           
        }
      
        public  void Register(UserRegisterRequest request)
        {
            userService.CreatePasswordHash(request.Password,
                                    out string PasswordHash,
                                    out string passwordSalt);
            var user = new User
            {
                Email = request.Email,
                Password = PasswordHash,
                PasswordSalt = passwordSalt,
                VerificationToken = userService.CreateRamdomToken(),
                PhoneNumber = request.Phone,
                RoleId = 2
            };
            webEcommerceContext.Users.Add(user);
        }
       
    }
}
