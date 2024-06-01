using Microsoft.AspNetCore.Identity;

namespace FahasaStoreAPI.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = null!;
        public string? PublicId { get; set; }
        public string ImageUrl { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
