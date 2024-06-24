using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using Web_Ecommerce_Server.Model.Entity;
using Web_Ecommerce_Server.Model.Request;
using Web_Ecommerce_Server.Service;

namespace Web_Ecommerce_Server.Reponsitory
{
    public class UserReponsitory : IUser
    {
        private readonly WebEcommerceContext webEcommerceContext;
        public UserReponsitory(WebEcommerceContext webEcommerceContext) 
        {
            this.webEcommerceContext = webEcommerceContext;
           
        }
        // dang nhap
        public Task Login(LoginRequest loginRequest)
        {
            throw new NotImplementedException();
        }
        // tao mat khau bam
        public void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = Convert.ToBase64String(hmac.Key);
                var hashBytes = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                passwordHash = Convert.ToBase64String(hashBytes);
            }
            
        }

        public string CreateRamdomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }

        public async void Register(UserRegisterRequest request)
        {
          CreatePasswordHash(request.Password,
                                    out string PasswordHash,
                                    out string passwordSalt);
            var user = new User
            {
                Email = request.Email,
                Password = PasswordHash,
                PasswordSalt = passwordSalt,
                VerificationToken = CreateRamdomToken(),
                PhoneNumber = request.Phone,
                RoleId = 2
            };
            webEcommerceContext.Users.Add(user);
        }
    }
}
