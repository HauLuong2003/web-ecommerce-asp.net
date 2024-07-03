namespace Web_Ecommerce_Server.Model.Entity
{
    public class Cart
    {
        public int UserId { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();
    }
}
