using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Text.Json;
using Web_Ecommerce_Server.Model.Entity;
using Web_Ecommerce_Server.Service;
using Microsoft.AspNetCore.Session;
using Web_Ecommerce_Server.Model.Request;
using System.Security.Claims;
using Web_Ecommerce_Server.Response;
using Newtonsoft.Json;
using Web_Ecommerce_Server.Model.DTO;
using Microsoft.EntityFrameworkCore;
using Web_Ecommerce_Server.Helper;

namespace Web_Ecommerce_Server.Reponsitory
{
    public class CartReponsitory : ICart
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession Session => _httpContextAccessor.HttpContext.Session;
        private readonly WebEcommerceContext _webEcommerceContext;


        public CartReponsitory(IHttpContextAccessor httpContextAccessor, IProduct productService, WebEcommerceContext webEcommerceContext)
        {
            _httpContextAccessor = httpContextAccessor;
           
            _webEcommerceContext = webEcommerceContext;

        }
        private async Task<bool> CartItemExists(int cartId, int productId)
        {
            return await _webEcommerceContext.CartItems.AnyAsync(c => c.CartId == cartId &&
                                                                     c.PId == productId);

        }
        public async Task<CartItem> AddItem(CartItemToAddDto cartItemToAddDto)
        {
            if (await CartItemExists(cartItemToAddDto.CartId, cartItemToAddDto.ProductId) == false)
            {
                var item = await (from product in _webEcommerceContext.Products
                                  where product.PId == cartItemToAddDto.ProductId
                                  select new CartItem
                                  {
                                      CartId = cartItemToAddDto.CartId,
                                      PId = product.PId,
                                      Quantity = product.Quantity,
                                  }).SingleOrDefaultAsync();
                if (item != null)
                {
                    var result = await _webEcommerceContext.CartItems.AddAsync(item);
                    await _webEcommerceContext.SaveChangesAsync();
                    return result.Entity;
                }
            }
            return null;
        }

        public async Task<CartItem> UpdateQty(int id, CartItemQtyUpdateDto cartItemQtyUpdateDto)
        {
            var item = await _webEcommerceContext.CartItems.FindAsync(id);

            if (item != null)
            {
                item.Quantity = cartItemQtyUpdateDto.Qty;
                await _webEcommerceContext.SaveChangesAsync();
                return item;
            }

            return null;
        }

        public Task<CartItem> DeleteItem(int id)
        {
            throw new NotImplementedException();
        }

            public async Task<IEnumerable<CartItem>> GetItems(int userId)
        {
            return await(from cart in _webEcommerceContext.Carts
                         join cartItem in _webEcommerceContext.CartItems
                         on cart.CartId equals cartItem.CartId
                         where cart.UserId == userId
                         select new CartItem
                         {
                             CartItemId = cartItem.CartItemId,
                             PId = cartItem.PId,
                             Quantity = cartItem.Quantity,
                             CartId = cartItem.CartId
                         }).ToListAsync();
        }

        public async Task<CartItem> GetItem(int id)
        {
            return await(from cart in _webEcommerceContext.Carts
                         join cartItem in _webEcommerceContext.CartItems
                         on cart.CartId equals cartItem.CartId
                         where cartItem.CartItemId == id
                         select new CartItem
                         {
                             CartItemId = cartItem.CartItemId,
                             PId = cartItem.PId,
                             Quantity = cartItem.Quantity,
                             CartId = cartItem.CartId
                         }).SingleAsync();
        }
    }
}
