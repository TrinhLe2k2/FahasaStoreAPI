using FahasaStoreAPI.Models.BasicModels;
using System;
using System.Collections.Generic;

namespace FahasaStoreAPI.Models.EntitiesModels
{
    public partial class BookEntities
    {
        public BookEntities()
        {
            CartItems = new HashSet<CartItemBasic>();
            FlashSaleBooks = new HashSet<FlashSaleBookBasic>();
            OrderItems = new HashSet<OrderItemBasic>();
            PosterImages = new HashSet<PosterImageBasic>();
            Reviews = new HashSet<ReviewBasic>();
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

        public virtual AuthorBasic? Author { get; set; }
        public virtual CoverTypeBasic? CoverType { get; set; }
        public virtual DimensionBasic? Dimension { get; set; }
        public virtual PartnerBasic? Partner { get; set; }
        public virtual SubcategoryBasic? Subcategory { get; set; }
        public virtual ICollection<CartItemBasic> CartItems { get; set; }
        public virtual ICollection<FlashSaleBookBasic> FlashSaleBooks { get; set; }
        public virtual ICollection<OrderItemBasic> OrderItems { get; set; }
        public virtual ICollection<PosterImageBasic> PosterImages { get; set; }
        public virtual ICollection<ReviewBasic> Reviews { get; set; }
    }
}
