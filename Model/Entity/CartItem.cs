using System.ComponentModel.DataAnnotations;

namespace Web_Ecommerce_Server.Model.Entity
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public float  Price { get; set; }
    }
}
