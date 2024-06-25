using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Ecommerce_Server.Model.Entity;
using Web_Ecommerce_Server.Model.Request;
using Web_Ecommerce_Server.Service;


namespace Web_Ecommerce_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserAccount userService;
       
      
        public UserController(IUserAccount userService) 
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
            var login = await userService.Login(request);
            return Ok(login);
        }
        [HttpPost("Verify")]
        public async Task<ActionResult> Verify(string token)
        {
           var verify = await userService.Verify(token);
            return Ok(verify);
        }
        [HttpPost("forget-password")]
        public async Task<ActionResult> ForgotPassword(string email)
        {
            var password = await userService.ForgotPassword(email);
            return Ok(password);
        }
        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPassword(ResetPassword resetPassword)
        {
            var resetpass = await userService.ResetPassword(resetPassword);
            return Ok(resetpass);
        }
    }
}
