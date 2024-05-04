namespace FahasaStoreAPI.Models.BasicModels
{
    public class NotificationBasic
    {
        public int NotificationId { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }
    }
}
