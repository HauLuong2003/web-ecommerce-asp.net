using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Web_Ecommerce_Server.Model.Entity;
using Web_Ecommerce_Server.Response;
using Web_Ecommerce_Server.Service;

namespace Web_Ecommerce_Server.Reponsitory
{
    public class ValidationService : IValidationService
    {
        private readonly WebEcommerceContext webEcommerceContext;
        public ValidationService(WebEcommerceContext webEcommerceContext)
        {
            this.webEcommerceContext = webEcommerceContext;
        }

        public async Task<ServiceResponse> CheckBrandNameAsync(string name)
        {
            var brand = await webEcommerceContext.Brands.FirstOrDefaultAsync(b => b.BrandName.ToLower()!.Equals(name.ToLower()));
            return brand is null ? new ServiceResponse(true, null!) : new ServiceResponse(false, "Product aldready exist");
        }

        public async Task<ServiceResponse> CheckProductNameAsync(string name)
        {
            var product = await webEcommerceContext.Products.FirstOrDefaultAsync(x => x.Name.ToLower()!.Equals(name.ToLower()));
            return product is null ? new ServiceResponse(true, null!) : new ServiceResponse(false, "Product aldready exist");
        }     
        public bool ProductExists(int id)
        {
            return webEcommerceContext.Products.Any(e => e.PId == id);
        }

        
    }
}
