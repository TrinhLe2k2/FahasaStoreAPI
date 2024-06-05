namespace FahasaStoreAPI.Models
{
    public class NotificationTypeModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
    }
}
