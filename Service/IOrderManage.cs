using Web_Ecommerce_Server.Model.Entity;
using Web_Ecommerce_Server.Response;

namespace Web_Ecommerce_Server.Service
{
    public interface IOrderManage
    {
        Task<List<Oder>> GetOrder();
        Task<List<Oder>> GetOderStatus(int status);
        Task<ServiceResponse>UpdateOrderStatus(int orderId, int status);
        Task<ServiceResponse> DeleteOrder(int orderId);
        Task<Oder>AddOrder(Oder order);
        Task<List<OrderStatus>> OrderStatus();
    }
}
