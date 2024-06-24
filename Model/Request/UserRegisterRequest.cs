using System.ComponentModel.DataAnnotations;

namespace Web_Ecommerce_Server.Model.Request
{
    public class UserRegisterRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required,MinLength(6)]
        public string Password { get; set; } = string.Empty;
        [Required,Compare("Password")]
        public string confirmPassword { get; set; } = string.Empty;
        [Required, StringLength(10, MinimumLength = 10)]
        public string Phone { get; set; } = string.Empty;
    }
}
