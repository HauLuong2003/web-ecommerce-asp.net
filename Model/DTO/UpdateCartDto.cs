namespace Web_Ecommerce_Server.Model.DTO
{
    public class UpdateCartDto
    {
        public string CartId { get; set; }
        public int ProductId {  get; set; }
        public int Quantity {  get; set; }
    }
}
