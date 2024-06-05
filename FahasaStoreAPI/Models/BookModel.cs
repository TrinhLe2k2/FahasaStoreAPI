namespace FahasaStoreAPI.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        public int SubcategoryId { get; set; }
        public int AuthorId { get; set; }
        public int CoverTypeId { get; set; }
        public int DimensionId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public int DiscountPercentage { get; set; }
        public int Quantity { get; set; }
        public double? Weight { get; set; }
        public int? PageCount { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
