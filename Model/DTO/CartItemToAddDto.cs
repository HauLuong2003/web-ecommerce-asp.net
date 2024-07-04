namespace Web_Ecommerce_Server.Model.DTO
{
    public class CartItemToAddDto
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Qty  { get; set; }
    }
}
