using Web_Ecommerce_Server.Response;

namespace Web_Ecommerce_Server.Service
{
    public interface ISaleReport
    {
        Task<List<ProductInventory>> productInventory();
        Task<List<Revenue>> GetRevenue(int day,int month, int year);
        Task<List<Revenue>> GetRevenueByMonth(int month,int year);
    }
}
