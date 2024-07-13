using Microsoft.EntityFrameworkCore;
using Web_Ecommerce_Server.Model;
using Web_Ecommerce_Server.Model.Entity;
using Web_Ecommerce_Server.Response;
using Web_Ecommerce_Server.Service;

namespace Web_Ecommerce_Server.Reponsitory
{
    public class UserReponsitory : IUser
    {
        private readonly WebEcommerceContext webEcommerceContext;
        public UserReponsitory(WebEcommerceContext webEcommerceContext)
        {
            this.webEcommerceContext = webEcommerceContext;
        }
        // xóa người dùng 
        public async Task<ServiceResponse> Delete(int id)
        {
            var userId = await webEcommerceContext.Users.FindAsync(id);
            if (userId == null)
            {
                return new ServiceResponse(false, "not found");
            }
             webEcommerceContext.Users.Remove(userId);
            await webEcommerceContext.SaveChangesAsync();
            return new ServiceResponse(true, "delete seccess");
        }
        //lay tat ca thong tin user
        public async Task<List<User>> GetAllUser()
        {
           var user = await webEcommerceContext.Users.ToListAsync();
            if(user == null)
            {
                throw new NotImplementedException("not found");
            }
            return user;
        }
        //lấy thông tin người dùng theo id
        public async Task<User> GetUserById(int id)
        {
            var userId = await webEcommerceContext.Users.FindAsync(id);
            if (userId == null)
            {
                throw new NotImplementedException("not found");
            }
            //tra ve gia thong tin user
            return userId;
        }
        //lấy thông tin người dùng theo name 
        public async Task<List<User>> GetUserByName(string name)
        {
            if (name == null)
            {
                throw new NotImplementedException("not name");
            }
            var nameUser = await webEcommerceContext.Users
                                        .Where(u => u.Fullname.Contains(name))
                                        .ToListAsync();
            if (nameUser == null)
            {
                throw new NotImplementedException("not found");
            }
            return nameUser;
        }
        //lấy thông tin người dùng theo số điện thoại
        public async Task<User> GetUserByPhone(string phone)
        {
            //neu thong tin sdt rong thi 
            if (!string.IsNullOrEmpty(phone))
            {
                throw new NotImplementedException("not phone");
            }
            //tim so dien thoai trong database
            var userPhone = await webEcommerceContext.Users
                .FirstOrDefaultAsync(u => u.PhoneNumber.Contains(phone));
            // neu khong tin thay so dien thoai
            if (userPhone == null)
            {
                throw new NotImplementedException("not found");
            }
            return userPhone;
        }
        // cập nhật thông tin người dùng
        public async Task<User> Update(int userId, User user)
        {
            if (user == null || userId <= 0)
            {
                throw new NotImplementedException("not user");
            }
            var users = await webEcommerceContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            if (users == null)
            {
                throw new NotImplementedException("not found");
            }
            users.Fullname = user.Fullname;
            users.PhoneNumber = user.PhoneNumber;
            users.Email = user.Email;
            users.Address = user.Address;
            users.Avata = user.Avata;
            return users;
        }
    }
}
