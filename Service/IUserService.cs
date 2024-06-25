namespace Web_Ecommerce_Server.Service
{
    public interface IUserService
    {
        //tao mat khau bam
        void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt);
        // tao token xac nhn
        string CreateRamdomToken();
        bool VerifyPasswordHash(string password, string passwordHash, string passwordSalt);

    }
}
