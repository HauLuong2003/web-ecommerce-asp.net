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
using Microsoft.EntityFrameworkCore;
using Web_Ecommerce_Server.Helper;
using Microsoft.AspNetCore.Mvc;

namespace Web_Ecommerce_Server.Reponsitory
{
    public class CartReponsitory : ICart
    {
        private readonly WebEcommerceContext _webEcommerceContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession session;
        public CartReponsitory( WebEcommerceContext webEcommerceContext, IHttpContextAccessor httpContextAccessor)
        {       
            _webEcommerceContext = webEcommerceContext;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<bool> CartItemExists(string cartId, int productId)
        {
            var cart =  _httpContextAccessor.HttpContext.Session.GetObjectFromJson<Cart>("Cart")?? new Cart { CartId = Guid.NewGuid().ToString() };

            if (cart != null)
            {
                return cart.Items.Any(item => item.cartId == cartId && item.PId == productId);
            }

            return false;
        }
        public async Task<ServiceResponse> AddItem(CartItemDto cartItemDto)
        {
            if ( await CartItemExists("Cart", cartItemDto.ProductId) == false)
            {
                string random = Guid.NewGuid().ToString();
                var cart = _httpContextAccessor.HttpContext.Session.GetObjectFromJson<Cart>("Cart") ?? new Cart { CartId = random };
                // var product = _webEcommerceContext.Products.FirstOrDefault(p => p.PId == productId);
                var cartItem = cart.Items.FirstOrDefault(i => i.PId == cartItemDto.ProductId);
                if (cartItem == null)
                {
                    cart.Items.Add(new CartItem()
                    {
                        PId = cartItemDto.ProductId,
                        cartId = random,
                        Quantity = cartItemDto.Quantity,
                        Price = cartItemDto.Price
                    });
                }
                else
                {
                    cartItem.Quantity += cartItemDto.Quantity;
                }
                _httpContextAccessor.HttpContext.Session.SetObjectAsJson("Cart", cart);

            }
            return new ServiceResponse(true, "add to sucesss");
        }
        //

        public async Task<CartItem> UpdateQty(UpdateCartDto updateCartDto)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var cart = session.GetObjectFromJson<Cart>("Cart") ;

            if (cart != null && cart.CartId ==updateCartDto.CartId  && cart.Items != null)
            {
                // Find the cart item to update based on productId
                var cartItemToUpdate = cart.Items.FirstOrDefault(item => item.PId == updateCartDto.ProductId);

                if (cartItemToUpdate != null)
                {
                    // Update the quantity of the cart item
                    cartItemToUpdate.Quantity = updateCartDto.Quantity;

                    // Update the cart in session
                    session.SetObjectAsJson("Cart", cart);
                    return cartItemToUpdate; // Quantity updated successfully
                }
            }

            return null; // Item not found or cart is empty
        }
        //
        public async Task<bool> DeleteItem(string cartId,int id)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var cart = session.GetObjectFromJson<Cart>("Cart") ?? new Cart { CartId = Guid.NewGuid().ToString() };

            if (cart != null && cart.CartId == cartId && cart.Items != null)
            {
                // Find the cart item to delete
                var itemToRemove = cart.Items.FirstOrDefault(item => item.PId == id);

                if (itemToRemove != null)
                {
                    // Remove the item from the cart
                    cart.Items.Remove(itemToRemove);

                    // Update the cart in session
                    session.SetObjectAsJson("Cart", cart);
                    return true;
                }
             
            }
            return false; // Return false if item was not found or cart is empty

        }

        public async Task<List<CartItem>> GetItemByUser(int userId,string cartId)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var cart = session.GetObjectFromJson<Cart>("Cart");

            if (cart != null &&cart.UserId == userId && cart.CartId == cartId && cart.Items != null)
            {
                // Optionally, you can filter cart items based on userId
                var userCartItems = cart.Items.Where(item => item.cartId == cartId).ToList(); // Adjust this to your retrieval logic

                if (userCartItems != null)
                {
                    // Optionally, you can return the cart items or their existence
                    return userCartItems; // Cart items found for the user
                }
                else
                {
                    return null;
                }
            }

            return null; // Cart items not found or cart is empty
        }

        public async Task<List<CartItem>> GetItem(string cartId)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var cart = session.GetObjectFromJson<Cart>("Cart");

            if (cart != null && cart.CartId == cartId && cart.Items != null)
            {
                // Optionally, you can retrieve the specific cart item based on some criteria
                // For example, find the first cart item matching a condition
                var cartItem = cart.Items.Where(item => item.cartId == cartId).ToList();
                // Optionally, you can return the cart item or its existence
                return cartItem; // Cart item found
                
            }

            return null; // Cart item not found or cart is empty
        }

        public async Task<ServiceResponse> AddToCart(int userId, CartItemDto cartItemDto)
        {
            string random = Guid.NewGuid().ToString();
            var session = _httpContextAccessor.HttpContext.Session;
            var cart = session.GetObjectFromJson<Cart>("Cart") ?? new Cart { CartId = random,UserId = userId };

            try
            {
                // Check if the product already exists in the cart
                var existingCartItem = cart.Items.FirstOrDefault(item => item.PId == cartItemDto.ProductId);

                if (existingCartItem != null)
                {
                    // Update the quantity of the existing cart item
                    existingCartItem.Quantity += cartItemDto.Quantity;
                }
                else
                {
                    // Add a new cart item to the cart
                    cart.Items.Add(new CartItem
                    {
                        PId = cartItemDto.ProductId,
                        cartId = random,
                        Quantity = cartItemDto.Quantity,
                        Price = cartItemDto.Price
                    });
                }

                // Update the cart in session
                session.SetObjectAsJson("Cart", cart);

                return new ServiceResponse(true, "Product added to cart successfully");
            }
            catch (Exception ex)
            {
                // Handle any exceptions, log them if needed
                return new ServiceResponse(false, $"Failed to add product to cart: {ex.Message}");
            }
        }
        public async Task ClearCart(string cartId)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            session.Remove("Cart");
        }

    }
}
