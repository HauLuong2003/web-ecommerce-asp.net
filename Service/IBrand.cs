using Ecommerce_Models.Model.Entity;
using Web_Ecommerce_Server.Response;

namespace Web_Ecommerce_Server.Service
{
    public interface IBrand
    {
        Task<List<Brand>> GetAllBrands();
        Task<Brand> GetBrandById(int id);
        Task<Brand> UpdateBrand(int id,Brand brand);
        Task DeleteBrand(int id);
        Task<ServiceResponse> AddBrand(Brand brand);
       
    }
}
