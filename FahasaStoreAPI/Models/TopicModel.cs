namespace FahasaStoreAPI.Models
{
    public class TopicModel
    {
        public int Id { get; set; }
        public string TopicName { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
    }
}
