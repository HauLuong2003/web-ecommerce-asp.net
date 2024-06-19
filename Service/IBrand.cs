using Web_Ecommerce_Server.Model.Entity;

namespace Web_Ecommerce_Server.Service
{
    public interface IBrand
    {
        Task<List<Brand>> GetAllBrands();
        Task<Brand> GetBrandById(int id);
        Task<Brand> UpdateBrand(string name);
    }
}
