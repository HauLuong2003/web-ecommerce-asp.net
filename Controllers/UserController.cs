using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
    
        public UserController(IUser userService, WebEcommerceContext webEcommerceContext) 
        { 
            this.userService = userService;
            this.webEcommerceContext = webEcommerceContext;
           
        }
        [HttpPost]
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
    }
}
