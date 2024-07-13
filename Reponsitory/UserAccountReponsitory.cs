using Azure.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Web_Ecommerce_Server.Model;
using Web_Ecommerce_Server.Model.Entity;
using Web_Ecommerce_Server.Model.Request;
using Web_Ecommerce_Server.Response;
using Web_Ecommerce_Server.Service;

namespace Web_Ecommerce_Server.Reponsitory
{
    public class UserAccountReponsitory : IUserAccount
    {
        private readonly WebEcommerceContext webEcommerceContext;
        private IUserService userService;
        private readonly IUserService IuserService;
        public UserAccountReponsitory(WebEcommerceContext webEcommerceContext, IUserService userService, IUserService IuserService) 
        {
            this.webEcommerceContext = webEcommerceContext;
           this.userService = userService;
            this.IuserService = IuserService;
        }
        // quen mat khau
        
        public async Task<ServiceResponse> ForgotPassword(string email)
        {
          var user = await webEcommerceContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return new ServiceResponse(false,"User not found");
            }
            user.ResetPasswordToken = userService.CreateRamdomToken();
            await webEcommerceContext.SaveChangesAsync();
            
            return new ServiceResponse(true, "User verified");
        }

        public async Task<ServiceResponse> Login(LoginRequest request)
        {
            var user = await webEcommerceContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
            {
                return new ServiceResponse(false,"user not found");
            }
            if (user.VerifiedAt == null)
            {
                return new ServiceResponse(false,"Not verified");
            }
            if (!IuserService.VerifyPasswordHash(request.Password, user.Password, user.PasswordSalt))
            {
                return new ServiceResponse(false,"password is incorrect.");
            }
            return new ServiceResponse(true,$"wellcome back,{user.Email}");
        }

        public async Task<ServiceResponse> Register(UserRegisterRequest request)
        {
            if (webEcommerceContext.Users.Any(u => u.Email == request.Email))
            {
                return new ServiceResponse(false, "User already exists");
            }
            if (webEcommerceContext.Users.Any(u => u.PhoneNumber == request.Phone))
            {
                return new ServiceResponse(false, "Phone already exists");
            }
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
            await webEcommerceContext.SaveChangesAsync();
            return new ServiceResponse(true, "dang ky thanh cong");
        }
        // dat lai mat khau
        public async Task<ServiceResponse> ResetPassword(ResetPassword resetPassword)
        {
           var user = await webEcommerceContext.Users.FirstOrDefaultAsync(u => u.ResetPasswordToken == resetPassword.Token);
            if( user == null)
            {
                return new ServiceResponse(false, "Invalid Token");
            }
            userService.CreatePasswordHash(resetPassword.Password, out string PasswordHash, out string passwordSalt);
            user.Password = PasswordHash;
            user.PasswordSalt = passwordSalt;
            user.ResetPasswordToken = null;
            await webEcommerceContext.SaveChangesAsync();
            return  new ServiceResponse(true, "you may now reset your password");
        }

        public async Task<ServiceResponse> Verify(string token)
        {
            var user = await webEcommerceContext.Users.FirstOrDefaultAsync(u => u.VerificationToken == token);
            if (user == null)
            {
                return new ServiceResponse(false,"Invalid token");
            }
            user.VerifiedAt = DateTime.Now;
            await webEcommerceContext.SaveChangesAsync();
            return new ServiceResponse(true, "User verified!");
        }
    }
}
