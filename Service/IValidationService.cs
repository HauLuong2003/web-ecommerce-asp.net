using Web_Ecommerce_Server.Response;

namespace Web_Ecommerce_Server.Service
{
    public interface IValidationService
    {
        Task<ServiceResponse> CheckProductNameAsync(string name);
        Task<ServiceResponse> CheckBrandNameAsync(string name);
        public bool ProductExists(int id);
       
    }
}
