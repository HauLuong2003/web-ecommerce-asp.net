using Microsoft.EntityFrameworkCore;
using Web_Ecommerce_Server.Model.Entity;
using Web_Ecommerce_Server.Service;

namespace Web_Ecommerce_Server.Reponsitory
{
    public class BrandReponsitory : IBrand
    {
        private readonly  WebEcommerceContext _context;
        public BrandReponsitory(WebEcommerceContext context)
        {
            _context = context;
        }
        //lay thuong hieu theo Id
        public Task<Brand> GetBrandById(int id)
        {
            throw new NotImplementedException();
        }
        // lay tat ca thuong hieu
        public async Task<List<Brand>> GetAllBrands()
        {
            return await _context.Brands.ToListAsync();
        }

        public Task<Brand> UpdateBrand(string name)
        {
            throw new NotImplementedException();
        }
    }
}
