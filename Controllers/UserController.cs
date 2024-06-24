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
        private readonly IUser userService;
        private readonly WebEcommerceContext webEcommerceContext;
        private readonly IUserService IuserService;
        public UserController(IUser userService, WebEcommerceContext webEcommerceContext, IUserService IuserService) 
        { 
            this.userService = userService;
            this.webEcommerceContext = webEcommerceContext;
            this.IuserService = IuserService;
        }
        [HttpPost("register")]
        public  async Task<ActionResult> Register(UserRegisterRequest request) 
        {
            if (webEcommerceContext.Users.Any(u => u.Email == request.Email))
            {
                return BadRequest("User already exists");
            }
            userService.Register(request);
            
            await webEcommerceContext.SaveChangesAsync();
            return Ok(new { message = "User registered successfully" });
        }
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginRequest request)
        {
            var user = await webEcommerceContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
            {
                return BadRequest("user not found");
            }
            if(user.VerifiedAt == null)
            {
                return BadRequest("Not verified");
            }
            if (!IuserService.VerifyPasswordHash(request.Password, user.Password, user.PasswordSalt)){
                return BadRequest("password is incorrect.");
            }
            return Ok($"wellcome back,{user.Email}");
        }
        [HttpPost("Verify")]
        public async Task<ActionResult> Verify(string token)
        {
            var user = await webEcommerceContext.Users.FirstOrDefaultAsync(u => u.VerificationToken == token);
            if(user == null)
            {
                return BadRequest("Invalid token");
            }
            user.VerifiedAt = DateTime.Now;
            await webEcommerceContext.SaveChangesAsync();
            return Ok("User verified!");
        }

    }
}
