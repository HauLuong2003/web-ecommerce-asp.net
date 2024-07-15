using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Ecommerce_Models.Model.Entity;
using Web_Ecommerce_Server.Reponsitory;
using Ecommerce_Models.Response;
using Ecommerce_Models.Service;

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
        [HttpGet("featured")]
        public async Task<ActionResult<List<Product>>> GetProductfeatured(bool featured)
        {
            try
            {
                var products = await productService.GetProductfeatured(featured);
                return Ok(products);
            }
            catch (Exception ex)
            {
                   return BadRequest(ex.Message);
            }
        }
        [HttpGet("getAll")]
        public async Task<ActionResult> GetAllProduct()
        {
            try
            {
                var products = await productService.GetAllProduct();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> AddProduct(Product model)
        {
            try
            {
                if (model is null)
                {
                    return BadRequest("model is null");
                }
                var response = await productService.AddProduct(model);
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
        [HttpGet("search-name")]
        public async Task<ActionResult> SearchProductByNam(string name)
        {
            try
            {
                var searchName = await productService.GetProductByName(name);
                return Ok(searchName);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("search-price")]
        public async Task<ActionResult> SearchProductByPrice(float price)
        {
            try
            {
                var products = await productService.GetProductByPrice(price);

                if (products == null || products.Count == 0)
                {
                    return NotFound("No products found for the given price.");
                }

                return Ok(products);
            }
            catch (Exception)
            {
                // Log the exception if necessary
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("search-brand")]
        public async Task<ActionResult> SearchProductByBrand(int brandId)
        {
            var searchBrand = await productService.GetProductByBrand(brandId);
            return Ok(searchBrand);
        }
    }
}
