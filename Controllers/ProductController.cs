using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Web_Ecommerce_Server.Model.Entity;
using Web_Ecommerce_Server.Response;
using Web_Ecommerce_Server.Service;

namespace Web_Ecommerce_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProduct productService;
        public ProductController(IProduct productService)
        {
            this.productService = productService;
        }
        [Produces(MediaTypeNames.Application.Json)]
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAllProducts(bool featured)
        {
            var products = await productService.GetAllProducts(featured);
            return Ok(products);
        }
        [HttpPost]
        public async Task<ActionResult> AddProduct(int brandid, Product model)
        {
            if (model is null)
            {
                return BadRequest("model is null");
            }
            var response = await productService.AddProduct(brandid,model);
            return Ok(response);
        }
        
        
        [HttpGet("{id}")]
        public async Task<ActionResult> GetProductById(int id)
        {
            var response = await productService.GetProductById(id);
            return Ok(response);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, Product product)
        {
            var updateProduct = await productService.UpdateProduct(id, product);
            return Ok(updateProduct);
        }
        [HttpDelete("{id}")]
        public async Task DeleteProduct(int id)
        {          
           await productService.DeleteProduct(id);
        }
    }
}
