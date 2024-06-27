using Web_Ecommerce_Server.Model.Entity;
using Web_Ecommerce_Server.Response;

namespace Web_Ecommerce_Server.Service
{
    public interface IUser
    {
        Task<User> Update(int userId,User user);
        Task<ServiceResponse> Delete(int id);
        Task<User> GetUserById(int id);
        Task<User> GetUserByPhone(string phone);
        Task<List<User>> GetUserByName(string name);
        Task<List<User>> GetAllUser();
    }
}
