using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Web_Ecommerce_Server.Model.Entity;
using Web_Ecommerce_Server.Response;
using Web_Ecommerce_Server.Service;

namespace Web_Ecommerce_Server.Reponsitory
{
    public class BrandReponsitory : IBrand
    {
        private readonly  WebEcommerceContext webEcommerceContext;
        private readonly IValidationService validationService;
        public BrandReponsitory(WebEcommerceContext webEcommerceContext, IValidationService validationService)
        {
            this.webEcommerceContext = webEcommerceContext;
            this.validationService = validationService;
        }
        //lay thuong hieu theo Id
        public async Task<Brand> GetBrandById(int id)
        {
           var brand = await webEcommerceContext.Brands.FindAsync(id);
            if (brand == null)
            {
                return null;
            }
            return brand;
        }
        // lay tat ca thuong hieu
        public async Task<List<Brand>> GetAllBrands()
        {
            return await webEcommerceContext.Brands.ToListAsync();
        }

        public async Task<Brand> UpdateBrand(int id,Brand brand)
        {
            var brands = await webEcommerceContext.Brands.FindAsync(id);
            if (brands == null)
            {
                return null;
            }
            brands.BrandName = brand.BrandName;
            brands.BrandLogo = brand.BrandLogo;
            await webEcommerceContext.SaveChangesAsync();
            return brands;
        }

        public async Task DeleteBrand(int id)
        {
            var brand = await GetBrandById(id);
            if (brand != null) // Check if the brand exists
            {
                webEcommerceContext.Brands.Remove(brand); // Remove the brand
                await webEcommerceContext.SaveChangesAsync(); // Save changes
            }

        }
        // them san pham
        public async Task<ServiceResponse> AddBrand(Brand brand)
        {
            if (brand is null) return new ServiceResponse(false, "brand is null");
            var (flag, message) = await validationService.CheckProductNameAsync(brand.BrandName!);
            if (flag)
            {
                webEcommerceContext.Brands.Add(brand);
                await validationService.CommitAsync();
                return new ServiceResponse(true, "brand save");
            }
            return new ServiceResponse(flag, message);
        }
    }
}
