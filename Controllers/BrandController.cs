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
        public BrandController(BrandReponsitory brandService)
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

    }
}
