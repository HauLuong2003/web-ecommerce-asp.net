using Microsoft.EntityFrameworkCore;
using Web_Ecommerce_Server.Model;
using Web_Ecommerce_Server.Response;
using Web_Ecommerce_Server.Service;

namespace Web_Ecommerce_Server.Reponsitory
{
    public class SaleReportReponsitory : ISaleReport
    {
        private readonly WebEcommerceContext webEcommerceContext;
        public SaleReportReponsitory(WebEcommerceContext webEcommerceContext)
        {
            this.webEcommerceContext = webEcommerceContext;
        }

        public async Task<List<ProductInventory>> productInventory()
        {
            var products = await webEcommerceContext.Products
                                          .Select(p => new ProductInventory
                                          {
                                              name = p.Name,                                   
                                              quantity = p.Quantity
                                          })
                                          .ToListAsync();
            if (products == null ) 
            {
                throw new NotImplementedException("not found");
             }
            return products;
        }


        public async Task<List<Revenue>> GetRevenueByDay(int day, int month, int year)
        {
            if (month < 1 || day < 1 || year < 1)
            {
                throw new NotImplementedException(" day month year don't ");
            }
            var revenueDetails = await (from oi in webEcommerceContext.OrderItems
                                        join o in webEcommerceContext.Oders on oi.OderId equals o.OrderId
                                        join p in webEcommerceContext.Products on oi.PId equals p.PId
                                        where o.OderDate.Day == day && o.OderDate.Year == year && o.OderDate.Month == month
                                        group new { oi, p } by new { p.Name, oi.Price, oi.Quantity, o.OderDate } into g
                                        select new Revenue
                                        {
                                            orderTime = g.Key.OderDate,
                                            name = g.Key.Name,
                                            price = g.Key.Price,
                                            quantity = g.Sum(x => x.oi.Quantity),
                                            totalRevenue = g.Sum(x => x.oi.Quantity * x.oi.Price)
                                        }).ToListAsync();

            if (revenueDetails == null || revenueDetails.Count == 0)
            {
                throw new NotImplementedException("Revenue details not found");
            }
            return revenueDetails;   
        }

        public async Task<List<Revenue>> GetRevenueByMonth(int? month, int year)
        {
           if(month == 0 || year == 0)
            {
                throw new NotImplementedException("khong chinh xac");
            }
                var revenue = await(from oi in webEcommerceContext.OrderItems
                                    join o in webEcommerceContext.Oders on oi.OderId equals o.OrderId
                                    join p in webEcommerceContext.Products on oi.PId equals p.PId
                                    where  o.OderDate.Month == month && o.OderDate.Year == year 
                                    group new { oi, p } by new { p.Name, oi.Price, oi.Quantity, o.OderDate } into g
                                    select new Revenue
                                    {
                                        orderTime = g.Key.OderDate,
                                        name = g.Key.Name,
                                        price = g.Key.Price,
                                        quantity = g.Sum(x => x.oi.Quantity),
                                        totalRevenue = g.Sum(x => x.oi.Quantity * x.oi.Price)
                                    }).ToListAsync();

                if (revenue == null)
                {
                    throw new NotImplementedException("not found");
                }
                return revenue;
            
        }
    }
}
