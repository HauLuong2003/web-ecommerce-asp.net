using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ecommerce_Models.Model.Entity;
using Ecommerce_Models.Service;

namespace Web_Ecommerce_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser user;
        public UserController (IUser user)
        {
            this.user = user;   
        }
        [HttpGet]
        public async Task<ActionResult> GetUser()
        {
            try
            {
                var getUser = await user.GetAllUser();
                return Ok(getUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetUserById(int id)
        {
            try
            {
                var getUserById = await user.GetUserById(id);
                return Ok(getUserById);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("user-phone")]
        public async Task<ActionResult> GetUserByPhone(string phone)
        {
            try
            {
                var getUserByPhone = await user.GetUserByPhone(phone);
                return Ok(getUserByPhone);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("user-name")]
        public async Task<ActionResult> GetUserByName(string name)
        {
            try
            {
                var getUserByName = await user.GetUserByName(name);
                return Ok(getUserByName);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete("id")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                var deleteUser = await user.Delete(id);
                return Ok(deleteUser);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut]
        public async Task<ActionResult> UpdateUser(int id, User users)
        {
            try
            {
                var update = await user.Update(id, users);
                return Ok(update);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
