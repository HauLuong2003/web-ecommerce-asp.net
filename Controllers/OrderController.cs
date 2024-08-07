﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ecommerce_Models.Model.Entity;
using Ecommerce_Models.Service;

namespace Web_Ecommerce_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        
        private readonly IOrderManage orderManage;
        public OrderController(IOrderManage orderManage)
        {
            this.orderManage = orderManage;
        }
        [HttpGet]
        public async Task<ActionResult> GetOrder()
        {
            try
            {
                var getorder = await orderManage.GetOrder();
                return Ok(getorder);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("status")]
        public async Task<ActionResult> GetOrderStatus(int status)
        {
            try
            {
                var getorderStatus = await orderManage.GetOderStatus(status);
                return Ok(getorderStatus);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteOrder(int OrderId)
        {
            try
            {
                var getorderStatus = await orderManage.DeleteOrder(OrderId);
                return Ok(getorderStatus);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> AddOrder(Oder oder)
        {
            try
            {
                var getorderStatus = await orderManage.AddOrder(oder);
                return Ok(getorderStatus);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getStatus")]
        public async Task<ActionResult> OrderStatus()
        {
            try
            {
                var orderStatus = await orderManage.OrderStatus();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<ActionResult> UpdateStatus(int orderId, int Status)
        {
            try
            {
                var updateStatuc = await orderManage.UpdateOrderStatus(orderId, Status);
                return Ok(updateStatuc);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
