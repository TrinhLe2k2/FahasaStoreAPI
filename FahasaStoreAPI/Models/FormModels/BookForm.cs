namespace FahasaStoreAPI.Models.FormModels
{
    public class BookForm
    {
        public BookForm(int bookId, int? subcategoryId, int? partnerId, int? authorId, int? coverTypeId, int? dimensionId, string name, string description, decimal originalPrice, decimal currentPrice, double discountPercentage, int quantity, double? weight, int? pageCount)
        {
            BookId = bookId;
            SubcategoryId = subcategoryId;
            PartnerId = partnerId;
            AuthorId = authorId;
            CoverTypeId = coverTypeId;
            DimensionId = dimensionId;
            Name = name;
            Description = description;
            OriginalPrice = originalPrice;
            CurrentPrice = currentPrice;
            DiscountPercentage = discountPercentage;
            Quantity = quantity;
            Weight = weight;
            PageCount = pageCount;
        }

        public int BookId { get; set; }
        public int? SubcategoryId { get; set; }
        public int? PartnerId { get; set; }
        public int? AuthorId { get; set; }
        public int? CoverTypeId { get; set; }
        public int? DimensionId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal OriginalPrice { get; set; }
        public decimal CurrentPrice { get; set; }
        public double DiscountPercentage { get; set; }
        public int Quantity { get; set; }
        public double? Weight { get; set; }
        public int? PageCount { get; set; }
    }
}
