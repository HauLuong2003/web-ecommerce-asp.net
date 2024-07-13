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
                throw new ArgumentException($"Brand with ID {id} not found.");

            }
            return brand;
        }
        // lay tat ca thuong hieu
        public async Task<List<Brand>> GetAllBrands()
        {
            var brand =  await webEcommerceContext.Brands.ToListAsync();
            return brand;
        }
        //update brand
        public async Task<Brand> UpdateBrand(int id,Brand brand)
        {
            var brands = await webEcommerceContext.Brands.FindAsync(id);
            if (brands == null)
            {
                throw new ArgumentException($"Brand not found.");
            }
            brands.BrandName = brand.BrandName;
            brands.BrandLogo = brand.BrandLogo;
            await webEcommerceContext.SaveChangesAsync();
            return brands;
        }
        // xoa brand
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
            var (flag, message) = await validationService.CheckBrandNameAsync(brand.BrandName!);
            if (flag)
            {
                webEcommerceContext.Brands.Add(brand);
                await webEcommerceContext.SaveChangesAsync();
                return new ServiceResponse(true, "brand save");
            }
            return new ServiceResponse(flag, message);
        }

       
    }
}
