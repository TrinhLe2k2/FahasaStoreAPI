using FahasaStoreAPI.Entities;
using FahasaStoreAPI.Identity;

namespace FahasaStoreAPI.Models.DTOModel
{
    public class LoginDTO
    {
        public LoginDTO()
        {
            this.UserId = "";
            this.Email = "";
            this.AccessToken = "";
            this.ImageUrl = "";
            this.FullName = "";
        }
        public LoginDTO(string userId, string email, string imageUrl, string fullName, string accessToken)
        {
            UserId = userId;
            Email = email;
            ImageUrl = imageUrl;
            AccessToken = accessToken;
            FullName = fullName;
        }

        public string UserId { get; }
        public string Email { get; }
        public string ImageUrl { get; }
        public string FullName { get; }
        public string AccessToken { get; }
    }
}
