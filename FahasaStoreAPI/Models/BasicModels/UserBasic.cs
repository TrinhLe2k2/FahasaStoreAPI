namespace FahasaStoreAPI.Models.BasicModels
{
    public class UserBasic
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public bool Active { get; set; }
    }
}
