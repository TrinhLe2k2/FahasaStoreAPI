using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Entities
{
    public partial class Book
    {
        public Book()
        {
            BooksPartners = new HashSet<BooksPartner>();
            CartItems = new HashSet<CartItem>();
            FlashSaleBooks = new HashSet<FlashSaleBook>();
            OrderItems = new HashSet<OrderItem>();
            PosterImages = new HashSet<PosterImage>();
            Reviews = new HashSet<Review>();
        }

        public int BookId { get; set; }
        public int? SubcategoryId { get; set; }
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

        public virtual Author? Author { get; set; }
        public virtual CoverType? CoverType { get; set; }
        public virtual Dimension? Dimension { get; set; }
        public virtual Subcategory? Subcategory { get; set; }
        public virtual ICollection<BooksPartner> BooksPartners { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<FlashSaleBook> FlashSaleBooks { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<PosterImage> PosterImages { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
