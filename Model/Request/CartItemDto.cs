namespace Web_Ecommerce_Server.Model.Request
{
    public class CartItemDto
    {
        public string? CartId { get; set; }
        public int ProductId { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
    }
}
