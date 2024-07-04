using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_Ecommerce_Server.Model.Entity;
using Web_Ecommerce_Server.Model.Request;
using Web_Ecommerce_Server.Reponsitory;
using Web_Ecommerce_Server.Service;

namespace Web_Ecommerce_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICart _shoppingCartService;

        public CartController(ICart shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }
      


    }
}
