
using Web_Ecommerce_Server.Model.DTO;
using Web_Ecommerce_Server.Model.Entity;
using Web_Ecommerce_Server.Model.Request;
using Web_Ecommerce_Server.Response;

namespace Web_Ecommerce_Server.Service
{
    public interface ICart
    {
        Task<CartItem> AddItem(CartItemToAddDto cartItemToAddDto);
        Task<CartItem> UpdateQty(int id, CartItemQtyUpdateDto cartItemQtyUpdateDto);
        Task<CartItem> DeleteItem(int id);
        Task<IEnumerable<CartItem>> GetItems(int userId);
        Task<CartItem> GetItem(int id);

    }
}
