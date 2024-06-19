using Microsoft.AspNetCore.Mvc;
using Web_Ecommerce_Server.Model.Entity;
using Web_Ecommerce_Server.Response;

namespace Web_Ecommerce_Server.Service
{
    public interface IProduct
    {
        //tao interface addProduct
        Task<ServiceResponse> AddProduct(Product model);
        //tao interface getAllProduct
        Task<List<Product>> GetAllProducts(bool featuredProducts);
        // tao interface getproduct detail
        Task<Product> GetProductById(int productId);
        Task<Product> UpdateProduct(int id, [FromBody] Product product);
        Task DeleteProduct(int id);
    }
}
