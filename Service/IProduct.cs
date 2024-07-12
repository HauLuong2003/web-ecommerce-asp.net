using Microsoft.AspNetCore.Mvc;
using Web_Ecommerce_Server.Model.Entity;
using Web_Ecommerce_Server.Response;

namespace Web_Ecommerce_Server.Service
{
    public interface IProduct
    {
        //tao interface addProduct
        Task<Product> AddProduct( Product model);
        //tao interface getAllProduct
        Task<List<Product>> GetProductfeatured(bool featuredProducts);
        Task<List<Product>> GetAllProduct();
        // tao interface getproduct detail
        Task<Product> GetProductById(int productId);
        Task<Product> UpdateProduct(int id, [FromBody] Product product);
        Task<ServiceResponse> DeleteProduct(int id);
        Task<List<Product>> GetProductByName(string name);
        Task<List<Product>> GetProductByBrand(int brandId);
        Task<List<Product>> GetProductByPrice(float price);
        Task<Price> GetProductPrice(int ProductId);
    }
}
