using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_Ecommerce_Server.Model.Entity;
using Web_Ecommerce_Server.Reponsitory;
using Web_Ecommerce_Server.Response;

namespace Web_Ecommerce_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly BrandReponsitory brandReponsitory;
        public BrandController(BrandReponsitory brandReponsitory)
        {
            this.brandReponsitory = brandReponsitory;
        }
        //tao controller cua get all brand
        [HttpGet]
        public async Task<ActionResult<List<Brand>>> GetAllBrand()
        {   
            
            var brand = await brandReponsitory.GetAllBrands();
            return Ok(brand);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse>> GetBrandById(int id)
        {
            var brandById = await brandReponsitory.GetBrandById(id);
            return Ok(brandById);
        }

    }
}
