using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_Ecommerce_Server.Service;

namespace Web_Ecommerce_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleReportController : ControllerBase
    {
        private readonly ISaleReport saleReport;
        public SaleReportController(ISaleReport saleReport)
        {
            this.saleReport = saleReport;
        }
        [HttpGet("Inventory")]
        public async Task<ActionResult> getInventory()
        {
            try
            {
                var inventory = await saleReport.productInventory();
                return Ok(inventory);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
       [HttpGet("Revenue-day")]
       public async Task<ActionResult> GetRevenueByDay(int day, int month, int year)
       {
           try
           {
               var Revenue = await saleReport.GetRevenueByDay(day, month, year);
               return Ok(Revenue);
           }
           catch (Exception ex)
           {
               return BadRequest(ex.Message);
           }
       }

       [HttpGet("Revenue-month")]
       public async Task<ActionResult> GetRevenueByMonth(int month, int year)
       {
           try
           {
               var Revenue = await saleReport.GetRevenueByMonth( month, year);
               return Ok(Revenue);
           }
           catch (Exception ex)
           {
               return BadRequest(ex.Message);
           }
       }
       
    }

}
