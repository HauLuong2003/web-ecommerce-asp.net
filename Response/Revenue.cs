namespace Web_Ecommerce_Server.Response
{
    public class Revenue
    {
        public DateTime orderTime {  get; set; }
        public string name { get; set; } = string.Empty;
        public double price { get; set; }
        public int quantity { get; set; }
        public double totalRevenue { get; set; }
    }
}
