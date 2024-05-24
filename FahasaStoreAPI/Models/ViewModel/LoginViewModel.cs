using System.ComponentModel.DataAnnotations;

namespace FahasaStoreAPI.Models.ViewModel
{
    public class LoginViewModel
    {
        public LoginViewModel(string email, string password, bool rememberMe)
        {
            Email = email;
            Password = password;
            RememberMe = rememberMe;
        }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
