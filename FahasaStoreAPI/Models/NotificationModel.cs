namespace FahasaStoreAPI.Models
{
    public class NotificationModel
    {
        public int Id { get; set; }
        public int NotificationTypeId { get; set; }
        public string? UserId { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public bool IsRead { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
