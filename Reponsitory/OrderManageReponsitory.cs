using Microsoft.EntityFrameworkCore;
using Web_Ecommerce_Server.Model.Entity;
using Web_Ecommerce_Server.Response;
using Web_Ecommerce_Server.Service;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Web_Ecommerce_Server.Reponsitory
{
    public class OrderManageReponsitory : IOrderManage
    {
        private readonly WebEcommerceContext webEcommerceContext;
        public OrderManageReponsitory(WebEcommerceContext webEcommerceContext)
        {
            this.webEcommerceContext = webEcommerceContext;
        }
        // lay thông tin đơn hàng  theo trang thai
        public async Task<List<Oder>> GetOderStatus(int status)
        { 
            var getOrder =  await webEcommerceContext.Oders.Where(o => o.Status == status).ToListAsync();
            if (getOrder == null)
            {
                throw new NotImplementedException("not found");
            }
            return getOrder;
            
        }
        // lay thông tin đơn hàng 
        public async Task<List<Oder>> GetOrder()
        {
            var order = await webEcommerceContext.Oders.ToListAsync();
            if(order == null)
            {
                throw new NotImplementedException("not found");
            }
            return order;
        }

        public async Task<ServiceResponse> DeleteOrder(int orderId)
        {
            var orderid = await webEcommerceContext.Oders.FindAsync(orderId);
            if(orderid == null)
            {
                throw new NotImplementedException("not found");
            }
            webEcommerceContext.Oders.Remove(orderid);
            return new ServiceResponse(true, "delete sucess");
        }

        public async Task<Oder> AddOrder(Oder order)
        {
            if (order == null)
            {
                throw new NotImplementedException("null");
            }
            var order1 = new Oder
            {
                Fullname = order.Fullname,
                Email = order.Email,
                PhoneNumber = order.PhoneNumber,
                Address = order.Address,
                Status = order.Status,
                OderDate = DateTime.Now,
                TotalMoney = order.TotalMoney,
                ShippingCost = order.ShippingCost,
                UserId = order.UserId,
                PaymentId = order.PaymentId,
                OrderItems = order.OrderItems.Select(oi => new OrderItem
                {
                    PId = oi.PId,
                    Price = oi.Price,
                    Quantity = oi.Quantity
                }).ToList()
            };
             webEcommerceContext.Add(order1);
            await webEcommerceContext.SaveChangesAsync();
            return order;
        }
    }
}
