using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Ecommerce_Server.Model.Entity;
using Web_Ecommerce_Server.Response;
using Web_Ecommerce_Server.Service;

namespace Web_Ecommerce_Server.Reponsitory
{
    public class ProductReponsitory : IProduct
    {
        private readonly WebEcommerceContext webEcommerceContext;
        private readonly IValidationService _validationService;

        public ProductReponsitory(WebEcommerceContext webEcommerceContext, IValidationService _validationService)
        {
            this.webEcommerceContext = webEcommerceContext;
            this._validationService = _validationService;
        }
        // them san pham moi
        public async Task<ServiceResponse> AddProduct(Product model)
        {
           if(model is null) return new ServiceResponse(false,"model is null");
            var (flag, message) = await _validationService.CheckProductNameAsync(model.Name!);
            if (flag)
            {
                webEcommerceContext.Products.Add(model);
                await _validationService.CommitAsync();
                return new ServiceResponse(true, "product save");
            }
            return new ServiceResponse(flag, message);
        }
        
        // lay san pham noi bat
        public async Task<List<Product>> GetAllProducts(bool featuredProducts)
        {
            if (featuredProducts)
            { 
                return await webEcommerceContext.Products.Where(_ => _.Featured).ToListAsync();
            }
            else
            {
                return await webEcommerceContext.Products.ToListAsync();
            }
        }
        
        // lay thong tin san pham
        public async Task<Product?> GetProductById(int productId)
        {
            var product  = await webEcommerceContext.Products
                                 .Include(d => d.Details) // Include related Product
                                 .FirstOrDefaultAsync(d => d.PId == productId);
            if (product == null)
            {
                return null;
            }
            return product;
        }
        //update san pham va chi tiet san pham
        public async Task<Product?> UpdateProduct(int id, [FromBody] Product product)
        {
            
            var products = await webEcommerceContext.Products
                .Include(p => p.Details)
                .FirstOrDefaultAsync(p => p.PId == id);
            if (products == null)
            {
                return null; // 
            }

            // Update product fields
            products.Name = product.Name;
            products.Description = product.Description;
            products.Quantity = product.Quantity;
            product.CreatAt = product.CreatAt;
            products.UpdateAt = product.UpdateAt;
            products.Featured = product.Featured;
            products.BrandId = product.BrandId;

            // Update details
            foreach (var detailDto in product.Details)
            {
                var detail = products.Details.FirstOrDefault(d => d.DetailId == detailDto.DetailId);
                if (detail != null)
                {
                    detail.SeriesLaptop = detailDto.SeriesLaptop;
                    detail.PartNumber = detailDto.PartNumber;
                    detail.Color = detailDto.Color;
                    detail.CpuGeneration = detailDto.CpuGeneration;
                    detail.Screen = detailDto.Screen;
                    detail.Storage = detailDto.Storage;
                    detail.ConnectorPort = detailDto.ConnectorPort;
                    detail.WirelessConnection = detailDto.WirelessConnection;
                    detail.Keyboard = detailDto.Keyboard;
                    detail.Os = detailDto.Os;
                    detail.Size = detailDto.Size;
                    detail.Pin = detailDto.Pin;
                    detail.Weight = detailDto.Weight;
                }
                else
                {
                    // Add new details if they don't exist
                    var newDetail = new Detail
                    {
                        SeriesLaptop = detailDto.SeriesLaptop,
                        PartNumber = detailDto.PartNumber,
                        Color = detailDto.Color,
                        CpuGeneration = detailDto.CpuGeneration,
                        Screen = detailDto.Screen,
                        Storage = detailDto.Storage,
                        ConnectorPort = detailDto.ConnectorPort,
                        WirelessConnection = detailDto.WirelessConnection,
                        Keyboard = detailDto.Keyboard,
                        Os = detailDto.Os,
                        Size = detailDto.Size,
                        Pin = detailDto.Pin,
                        Weight = detailDto.Weight,
                        PId = detailDto.PId
                    };
                    product.Details.Add(newDetail);
                }
            }

            try
            {
                await webEcommerceContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_validationService.ProductExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return product;
        }
        // delete Product and detail
        public async Task DeleteProduct(int id)
        {
            var product = await GetProductById(id);
           
            foreach (var detail in product.Details.ToList())
            {
                webEcommerceContext.Details.Remove(detail);
            }

            webEcommerceContext.Products.Remove(product);
            
             await webEcommerceContext.SaveChangesAsync();
 
        }
    }

}


