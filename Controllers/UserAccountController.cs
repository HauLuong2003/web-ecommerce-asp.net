using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ecommerce_Models.Model.Entity;
using Ecommerce_Models.Model.Request;
using Web_Ecommerce_Server.Service;

namespace Web_Ecommerce_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        private readonly IUserAccount userService;
       
      
        public UserAccountController(IUserAccount userService) 
        { 
            this.userService = userService;
                      
        }
        [HttpPost("register")]
        public  async Task<ActionResult> Register(UserRegisterRequest request) 
        {
            
            var register = await userService.Register(request);         
            return Ok(register);
        }
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginRequest request)
        {
            try
            {
                var login = await userService.Login(request);
                return Ok(login);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost("Verify")]
        public async Task<ActionResult> Verify(string token)
        {
            try { 
           var verify = await userService.Verify(token);
            return Ok(verify);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("forget-password")]
        public async Task<ActionResult> ForgotPassword(string email)
        {
            try
            {
                var password = await userService.ForgotPassword(email);
                return Ok(password);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPassword(ResetPassword resetPassword)
        {
            try
            {
                var resetpass = await userService.ResetPassword(resetPassword);
                return Ok(resetpass);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
