using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ecommerce_Models.Model.Entity;
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
            try
            {
                var brand = await brandService.GetAllBrands();
                return Ok(brand);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse>> GetBrandById(int id)
        {
            try
            {
                var brandById = await brandService.GetBrandById(id);
                return Ok(brandById);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //update Brand
        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse>> UpdateBrand(int id, Brand brand)
        {
            try
            {
                var brands = await brandService.UpdateBrand(id, brand);
                return Ok(brands);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //delete brand
        [HttpDelete("{id}")]
        public async Task DeleteBrand(int id)
        {
            try
            {
                await brandService.DeleteBrand(id);
            }
            catch(Exception ex)
            {
                 BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> AddBrand(Brand brand)
        {
            try
            {
                if (brand == null)
                {
                    return BadRequest("brand is nuull");
                }
                var response = await brandService.AddBrand(brand);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
