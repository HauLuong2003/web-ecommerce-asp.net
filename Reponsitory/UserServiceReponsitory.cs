using System.Security.Cryptography;
using System.Text;
using Web_Ecommerce_Server.Model;
using Ecommerce_Models.Service;

namespace Web_Ecommerce_Server.Reponsitory
{
    public class UserServiceReponsitory : IUserService

    {
        private readonly WebEcommerceContext webEcommerceContext;
        public UserServiceReponsitory (WebEcommerceContext webEcommerceContext)
        {
            this.webEcommerceContext = webEcommerceContext;
        }
        // tao mat khau bam
        public void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = Convert.ToBase64String(hmac.Key);
                var hashBytes = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                passwordHash = Convert.ToBase64String(hashBytes);
            }

        }

        public string CreateRamdomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }
        // xac nhan mat khau mang bam
        public bool VerifyPasswordHash(string password, string passwordHash, string passwordSalt)
        {
            using (var hmac = new HMACSHA512(Convert.FromBase64String(passwordSalt)))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert the stored password hash from Base64 string to byte array
                var passwordHashBytes = Convert.FromBase64String(passwordHash);
                  
                // Compare the computed hash with the stored hash
                return computedHash.SequenceEqual(passwordHashBytes);
            }

        }
    }
}
