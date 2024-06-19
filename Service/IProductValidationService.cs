using Web_Ecommerce_Server.Response;

namespace Web_Ecommerce_Server.Service
{
    public interface IProductValidationService
    {
        Task<ServiceResponse> CheckProductNameAsync(string name);
        Task<int> CommitAsync();
        public bool ProductExists(int id);
    }
}
