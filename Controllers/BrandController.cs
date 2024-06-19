using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_Ecommerce_Server.Model.Entity;
using Web_Ecommerce_Server.Reponsitory;

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
    }
}
