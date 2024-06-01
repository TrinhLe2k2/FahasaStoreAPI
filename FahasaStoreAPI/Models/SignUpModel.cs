using System.ComponentModel.DataAnnotations;

namespace FahasaStoreAPI.Models
{
    public class SignUpModel
    {
        public SignUpModel(string email, string password, string confirmPassword)
        {
            Email = email;
            Password = password;
            ConfirmPassword = confirmPassword;
        }
        [Required]
        public string FullName { get; set; } = null!;
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required, Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
        public string ConfirmPassword { get; set; }
    }
}
