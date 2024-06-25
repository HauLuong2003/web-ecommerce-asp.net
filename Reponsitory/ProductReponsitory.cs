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
        public async Task<Product> AddProduct(int brandid,Product model)
        {
            var brand = await webEcommerceContext.Brands.FindAsync(brandid);
            if (brand == null)
            {
                throw new ArgumentException($"Brand with ID {brandid} not found.");
            }
            var product = new Product
            {
                Name = model.Name,
                Description = model.Description,
                Quantity = model.Quantity,
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow,
                Featured = model.Featured,
                BrandId = brandid, // Set BrandId to link with existing Brand
                Image1 = model.Image1,
                Image2 = model.Image2,
                Image3 = model.Image3
               
            };
            // Add Prices associated with the product
            foreach (var priceRequest in model.Prices)
            {
                var price = new Price
                {
                    Price1 = priceRequest.Price1,
                    CreateAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow
                };
                product.Prices.Add(price);
            }
            // Add Details associated with the product
            foreach (var detailRequest in model.Details)
            {
                var detail = new Detail
                {
                    SeriesLaptop = detailRequest.SeriesLaptop,
                    PartNumber = detailRequest.PartNumber,
                    Color = detailRequest.Color,
                    CpuGeneration = detailRequest.CpuGeneration,
                    Screen = detailRequest.Screen,
                    Storage = detailRequest.Storage,
                    ConnectorPort = detailRequest.ConnectorPort,
                    WirelessConnection = detailRequest.WirelessConnection,
                    Keyboard = detailRequest.Keyboard,
                    Os = detailRequest.Os,
                    Size = detailRequest.Size,
                    Pin = detailRequest.Pin,
                    Weight = detailRequest.Weight
                };
                product.Details.Add(detail);
            }
            webEcommerceContext.Products.Add(product);
            await webEcommerceContext.SaveChangesAsync();
            return product;

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
        public async Task<Product> GetProductById(int productId)
        {
            var product  = await webEcommerceContext.Products
                                 .Include(d => d.Details)
                                 .Include(p => p.Prices)// Include related Product
                                 .FirstOrDefaultAsync(d => d.PId == productId);
            if (product == null)
            {
                throw new ArgumentException($"product with ID {productId} not found.");
            }
            return product;
        }
        //update san pham va chi tiet san pham
        public async Task<Product> UpdateProduct(int id, [FromBody] Product product)
        {
            
            var products = await webEcommerceContext.Products
                .Include(p => p.Details)
                .Include(p => p.Prices)
                .FirstOrDefaultAsync(p => p.PId == id);
            if (products == null)
            {
                throw new ArgumentException($"product not found.");

            }

            // Update product fields
            products.Name = product.Name;
            products.Description = product.Description;
            products.Quantity = product.Quantity;
            product.CreateAt = product.CreateAt;
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
            //update price
            foreach(var prices in product.Prices)
            {
                var price = products.Prices.FirstOrDefault(p => p.PriceId == prices.PriceId);
                if (price != null)
                {
                    price.Price1 = prices.Price1;
                    
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
                    throw new ArgumentException("not found");
                }
                else
                {
                    throw;
                }
            }

            return product;
        }
        // delete Product and detail
        public async Task<ServiceResponse> DeleteProduct(int id)
        {
            var product = await GetProductById(id);
           if(product == null)
            {
                return new ServiceResponse(false, "not found");
            }
            foreach (var detail in product.Details.ToList())
            {
                webEcommerceContext.Details.Remove(detail);
            }

            webEcommerceContext.Products.Remove(product);
            
             await webEcommerceContext.SaveChangesAsync();
            return new ServiceResponse(true, "delete");
        }

      

        Task<List<Product>> IProduct.GetProductByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetProductByBrand(int brandId)
        {
            throw new NotImplementedException();
        }
    }

}


