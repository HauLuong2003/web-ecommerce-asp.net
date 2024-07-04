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

namespace Web_Ecommerce_Server.Reponsitory
{
    public class CartReponsitory : ICart
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession Session => _httpContextAccessor.HttpContext.Session;


        private readonly IProduct productService;
        public CartReponsitory(IHttpContextAccessor httpContextAccessor, IProduct productService)
        {
            _httpContextAccessor = httpContextAccessor;
            this.productService= productService;
        }

        public Task<CartItem> AddItem(CartItemToAddDto cartItemToAddDto)
        {
            throw new NotImplementedException();
        }

        public Task<CartItem> UpdateQty(int id, CartItemQtyUpdateDto cartItemQtyUpdateDto)
        {
            throw new NotImplementedException();
        }

        public Task<CartItem> DeleteItem(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CartItem> GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CartItem>> GetItems(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
