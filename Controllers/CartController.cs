using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_Ecommerce_Server.Helper;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CartController(ICart shoppingCartService, IHttpContextAccessor contextAccessor)
        {
            _shoppingCartService = shoppingCartService;
            _httpContextAccessor = contextAccessor;
        }
        [HttpGet("get-cart")]
        public ActionResult<Cart> GetCart()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var cart = session.GetObjectFromJson<Cart>("Cart");
            return Ok(cart);
        }

        [HttpPost("add-to-cart")]
        public async Task<ActionResult> AddToCart(CartItemDto cartItemDto) 
        {
            var add = await _shoppingCartService.AddItem(cartItemDto);
            return Ok(add);
        }
        // xoa item
        [HttpDelete("delete-item")]
        public async Task<ActionResult> DeleteCartItem(string cartId,int productId)
        {
            var deleted = await _shoppingCartService.DeleteItem(cartId,productId);

            if (deleted)
            {
                return Ok("Item deleted successfully");
            }
            else
            {
                return NotFound("Item not found in cart");
            }
        }
        [HttpPut("update-quantity")]
        public async Task<ActionResult> UpdateCartItemQuantity(UpdateCartDto updateCartDto)
        {
            var updated = await _shoppingCartService.UpdateQty(updateCartDto);

            return Ok(updated);
        }
        [HttpGet("get-item/{cartId}")]
        public async Task<ActionResult> GetCartItem(string cartId)
        {
            var found = await _shoppingCartService.GetItem(cartId);
                return Ok(found);
          
        }

        [HttpPost("add-to-cart/{userId}")]
        public async Task<ActionResult> AddToCart(int userId,  CartItemDto cartItemDto)
        {
            var response = await _shoppingCartService.AddToCart(userId, cartItemDto);

            return Ok(response);
        }
        [HttpGet("get-items-by-user")]
        public async Task<ActionResult> GetCartItemsByUser(int userId, string cartId)
        {
            var found = await _shoppingCartService.GetItemByUser(userId, cartId);
             return Ok(found);

        }
        [HttpDelete("delete-cart")]
        public async Task<ActionResult>DeleteCart(string cartId)
        {
            var deleteCart =  _shoppingCartService.ClearCart(cartId);
            return Ok(deleteCart);
        }
    }
}
