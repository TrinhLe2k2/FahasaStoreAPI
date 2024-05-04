using FahasaStoreAPI.Entities;
using FahasaStoreAPI.Models.BasicModels;

namespace FahasaStoreAPI.Models.EntitiesModels
{
    public class BookEntities
    {
        public BookEntities()
        {
        }

        public BookEntities(int bookId, string name, string description, decimal originalPrice, decimal currentPrice, double discountPercentage, int quantity, double? weight, int? pageCount, AuthorBasic author, CoverTypeBasic coverType, DimensionBasic dimension, PartnerBasic partner, SubcategoryBasic subcategory)
        {
            BookId = bookId;
            Name = name;
            Description = description;
            OriginalPrice = originalPrice;
            CurrentPrice = currentPrice;
            DiscountPercentage = discountPercentage;
            Quantity = quantity;
            Weight = weight;
            PageCount = pageCount;
            Author = author;
            CoverType = coverType;
            Dimension = dimension;
            Partner = partner;
            Subcategory = subcategory;
        }

        public BookEntities(int bookId, string name, string description, decimal originalPrice, decimal currentPrice, double discountPercentage, int quantity, double? weight, int? pageCount, AuthorBasic? author, CoverTypeBasic? coverType, DimensionBasic? dimension, PartnerBasic? partner, SubcategoryBasic? subcategory, ICollection<CartItemBasic> cartItems, ICollection<FlashSaleBookBasic> flashSaleBooks, ICollection<OrderItemBasic> orderItems, ICollection<PosterImageBasic> posterImages, ICollection<ReviewBasic> reviews)
        {
            BookId = bookId;
            Name = name;
            Description = description;
            OriginalPrice = originalPrice;
            CurrentPrice = currentPrice;
            DiscountPercentage = discountPercentage;
            Quantity = quantity;
            Weight = weight;
            PageCount = pageCount;
            Author = author;
            CoverType = coverType;
            Dimension = dimension;
            Partner = partner;
            Subcategory = subcategory;
            CartItems = cartItems;
            FlashSaleBooks = flashSaleBooks;
            OrderItems = orderItems;
            PosterImages = posterImages;
            Reviews = reviews;
        }

        public int BookId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal OriginalPrice { get; set; }
        public decimal CurrentPrice { get; set; }
        public double DiscountPercentage { get; set; }
        public int Quantity { get; set; }
        public double? Weight { get; set; }
        public int? PageCount { get; set; }

        public virtual AuthorBasic? Author { get; set; }
        public virtual CoverTypeBasic? CoverType { get; set; }
        public virtual DimensionBasic? Dimension { get; set; }
        public virtual PartnerBasic? Partner { get; set; }
        public virtual SubcategoryBasic? Subcategory { get; set; }

        public virtual ICollection<CartItemBasic>? CartItems { get; set; }
        public virtual ICollection<FlashSaleBookBasic>? FlashSaleBooks { get; set; }
        public virtual ICollection<OrderItemBasic>? OrderItems { get; set; }
        public virtual ICollection<PosterImageBasic>? PosterImages { get; set; }
        public virtual ICollection<ReviewBasic>? Reviews { get; set; }
    }
}
