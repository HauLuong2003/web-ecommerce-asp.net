using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_Ecommerce_Server.Model.Entity;
using Web_Ecommerce_Server.Reponsitory;
using Web_Ecommerce_Server.Response;
using Web_Ecommerce_Server.Service;

namespace Web_Ecommerce_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrand brandService;
        public BrandController(IBrand brandService)
        {
            this.brandService = brandService;
        }
        //tao controller cua get all brand
        [HttpGet]
        public async Task<ActionResult<List<Brand>>> GetAllBrand()
        {   
            
            var brand = await brandService.GetAllBrands();
            return Ok(brand);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse>> GetBrandById(int id)
        {
            var brandById = await brandService.GetBrandById(id);
            return Ok(brandById);
        }
        //update Brand
        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse>> UpdateBrand(int id, Brand brand)
        {
            var brands = await brandService.UpdateBrand(id, brand);
            return Ok(brands);
        }
        //delete brand
        [HttpDelete("{id}")]
        public async Task DeleteBrand(int id)
        {
            await brandService.DeleteBrand(id);
           
        }
    }
}
