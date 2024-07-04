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
       
        public async Task<CartItem> AddItem(CartItemToAddDto cartItemToAddDto)
        {
            var cartId = GetCartId();

            var cartItem = new CartItem
            {
                CartId = cartId,
                PId = cartItemToAddDto.ProductId,
                Quantity = cartItemToAddDto.Qty
            };

            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                await _webEcommerceContext.CartItems.AddAsync(cartItem);
                await _webEcommerceContext.SaveChangesAsync();
            }
            else
            {
                var sessionCartItems = Session.GetObjectFromJson<List<CartItem>>("CartItems") ?? new List<CartItem>();
                sessionCartItems.Add(cartItem);
                Session.SetObjectAsJson("CartItems", sessionCartItems);
            }

            return cartItem;
        }

        public async Task<CartItem> UpdateQty(int id, CartItemQtyUpdateDto cartItemQtyUpdateDto)
        {
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var cartItem = await _webEcommerceContext.CartItems.FindAsync(id);
                if (cartItem != null)
                {
                    cartItem.Quantity = cartItemQtyUpdateDto.Qty;
                    await _webEcommerceContext.SaveChangesAsync();
                }
                return cartItem;
            }
            else
            {
                var sessionCartItems = Session.GetObjectFromJson<List<CartItem>>("CartItems");
                var cartItem = sessionCartItems?.FirstOrDefault(ci => ci.CartItemId == id);
                if (cartItem != null)
                {
                    cartItem.Quantity = cartItemQtyUpdateDto.Qty;
                    Session.SetObjectAsJson("CartItems", sessionCartItems);
                }
                return cartItem;
            }
        }

        public async Task<CartItem> DeleteItem(int id)
        {
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var cartItem = await _webEcommerceContext.CartItems.FindAsync(id);
                if (cartItem != null)
                {
                    _webEcommerceContext.CartItems.Remove(cartItem);
                    await _webEcommerceContext.SaveChangesAsync();
                }
                return cartItem;
            }
            else
            {
                var sessionCartItems = Session.GetObjectFromJson<List<CartItem>>("CartItems");
                var cartItem = sessionCartItems?.FirstOrDefault(ci => ci.CartItemId == id);
                if (cartItem != null)
                {
                    sessionCartItems.Remove(cartItem);
                    Session.SetObjectAsJson("CartItems", sessionCartItems);
                }
                return cartItem;
            }
        }

        public async Task<CartItem> GetItem(int id)
        {
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                return await (from cart in _webEcommerceContext.Carts
                              join cartItem in _webEcommerceContext.CartItems
                              on cart.CartId equals cartItem.CartId
                              where cartItem.CartItemId == id
                              select new CartItem
                              {
                                  CartItemId = cartItem.CartItemId,
                                  PId = cartItem.PId,
                                  Quantity = cartItem.Quantity,
                                  CartId = cartItem.CartId,
                                  Cart = cart,
                                  PIdNavigation = cartItem.PIdNavigation
                              }).SingleAsync();
            }
            else
            {
                var sessionCartItems = Session.GetObjectFromJson<List<CartItem>>("CartItems");
                return sessionCartItems?.FirstOrDefault(ci => ci.CartItemId == id);
            }
        }

        public async Task<IEnumerable<CartItem>> GetCart(int userId)
        {
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                return await (from cart in _webEcommerceContext.Carts
                              join cartItem in _webEcommerceContext.CartItems
                              on cart.CartId equals cartItem.CartId
                              where cart.UserId == userId
                              select new CartItem
                              {
                                  CartItemId = cartItem.CartItemId,
                                  PId = cartItem.PId,
                                  Quantity = cartItem.Quantity,
                                  CartId = cartItem.CartId,
                                  Cart = cart,
                                  PIdNavigation = cartItem.PIdNavigation
                              }).ToListAsync();
            }
            else
            {
                return Session.GetObjectFromJson<List<CartItem>>("CartItems") ?? new List<CartItem>();
            }
        }
        
        private int GetCartId()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                return int.Parse(userId);
            }

            var sessionCartId = Session.GetInt32("CartId");
            if (sessionCartId == null)
            {
                sessionCartId = new Random().Next(1, int.MaxValue);
                Session.SetInt32("CartId", (int)sessionCartId);
            }
            return (int)sessionCartId;
        }

    }
}
