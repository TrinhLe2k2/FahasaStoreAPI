namespace FahasaStoreAPI.Models
{
    public class BookPartnerModel
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int PartnerId { get; set; }
        public string? Note { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
