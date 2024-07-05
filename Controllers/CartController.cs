using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_Ecommerce_Server.Model.DTO;
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
        [HttpPost("AddItem")]
        public async Task<ActionResult<CartItem>> AddItem(CartItemToAddDto cartItemToAddDto)
        {
            var item = await _shoppingCartService.AddItem(cartItemToAddDto);
            return CreatedAtAction(nameof(GetItem), new { id = item.PId }, item);
        }

        [HttpPut("UpdateQty/{id}")]
        public async Task<ActionResult<CartItem>> UpdateQty(int id, CartItemQtyUpdateDto cartItemQtyUpdateDto)
        {
            var item = await _shoppingCartService.UpdateQty(id, cartItemQtyUpdateDto);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpDelete("DeleteItem/{id}")]
        public async Task<ActionResult<CartItem>> DeleteItem(int id)
        {
            var item = await _shoppingCartService.DeleteItem(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpGet("GetItem/{id}")]
        public async Task<ActionResult<CartItem>> GetItem(int id)
        {
            var item = await _shoppingCartService.GetItem(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpGet("GetItems/{userId}")]
        public async Task<ActionResult<IEnumerable<CartItem>>> GetItems(int userId)
        {
            var items = await _shoppingCartService.GetItems(userId);
            return Ok(items);
        }


    }
}
