using FahasaStoreAPI.Entities;

namespace FahasaStoreAPI.Models.DTO
{
    public partial class AddressDTO
    {
        public AddressDTO()
        {
            Orders = new HashSet<OrderModel>();
        }

        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public string ReceiverName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Province { get; set; } = null!;
        public string District { get; set; } = null!;
        public string Ward { get; set; } = null!;
        public string Detail { get; set; } = null!;
        public bool DefaultAddress { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual AspNetUserModel User { get; set; } = null!;
        public virtual ICollection<OrderModel> Orders { get; set; }
    }
    public partial class AspNetRoleDTO
    {
        public AspNetRoleDTO()
        {
            Users = new HashSet<AspNetUserModel>();
        }

        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string? NormalizedName { get; set; }
        public string? ConcurrencyStamp { get; set; }

        public virtual ICollection<AspNetUserModel> Users { get; set; }
    }
    public partial class AspNetUserDTO
    {
        public AspNetUserDTO()
        {
            Addresses = new HashSet<AddressModel>();
            Favourites = new HashSet<FavouriteModel>();
            Notifications = new HashSet<NotificationModel>();
            Orders = new HashSet<OrderModel>();
            Reviews = new HashSet<ReviewModel>();
            Roles = new HashSet<AspNetRoleModel>();
        }

        public string Id { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string? PublicId { get; set; }
        public string ImageUrl { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public string? UserName { get; set; }
        public string? NormalizedUserName { get; set; }
        public string? Email { get; set; }
        public string? NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string? PasswordHash { get; set; }
        public string? SecurityStamp { get; set; }
        public string? ConcurrencyStamp { get; set; }
        public string? PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }

        public virtual CartModel? Cart { get; set; }
        public virtual ICollection<AddressModel> Addresses { get; set; }
        public virtual ICollection<FavouriteModel> Favourites { get; set; }
        public virtual ICollection<NotificationModel> Notifications { get; set; }
        public virtual ICollection<OrderModel> Orders { get; set; }
        public virtual ICollection<ReviewModel> Reviews { get; set; }

        public virtual ICollection<AspNetRoleModel> Roles { get; set; }
    }
    public partial class AuthorDTO
    {
        public AuthorDTO()
        {
            Books = new HashSet<BookModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }

        public virtual ICollection<BookModel> Books { get; set; }
    }
    public partial class BannerDTO
    {
        public int Id { get; set; }
        public string? PublicId { get; set; }
        public string? ImageUrl { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
    }
    public partial class BookDTO
    {
        public BookDTO()
        {
            BookPartners = new HashSet<BookPartnerDTO>();
            CartItems = new HashSet<CartItemModel>();
            Favourites = new HashSet<FavouriteModel>();
            FlashSaleBooks = new HashSet<FlashSaleBookModel>();
            OrderItems = new HashSet<OrderItemModel>();
            PosterImages = new HashSet<PosterImageModel>();
            Reviews = new HashSet<ReviewModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Price { get; set; }
        public int DiscountPercentage { get; set; }
        public int Quantity { get; set; }
        public double? Weight { get; set; }
        public int? PageCount { get; set; }
        public int? QuantitySold { get; set; }
        public DateTime? CreatedAt { get; set; }


        public virtual AuthorModel Author { get; set; } = null!;
        public virtual CoverTypeModel CoverType { get; set; } = null!;
        public virtual DimensionModel Dimension { get; set; } = null!;
        public virtual SubcategoryModel Subcategory { get; set; } = null!;
        public virtual ICollection<BookPartnerDTO> BookPartners { get; set; }
        public virtual ICollection<CartItemModel> CartItems { get; set; }
        public virtual ICollection<FavouriteModel> Favourites { get; set; }
        public virtual ICollection<FlashSaleBookModel> FlashSaleBooks { get; set; }
        public virtual ICollection<OrderItemModel> OrderItems { get; set; }
        public virtual ICollection<PosterImageModel> PosterImages { get; set; }
        public virtual ICollection<ReviewModel> Reviews { get; set; }
    }
    public partial class BookPartnerDTO
    {
        public int Id { get; set; }
        public string? Note { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual BookModel Book { get; set; } = null!;
        public virtual PartnerDTO Partner { get; set; } = null!;
    }
    public partial class CartDTO
    {
        public CartDTO()
        {
            CartItems = new HashSet<CartItemModel>();
        }

        public int Id { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual AspNetUserModel User { get; set; } = null!;
        public virtual ICollection<CartItemModel> CartItems { get; set; }
    }
    public partial class CartItemDTO
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual BookDTO Book { get; set; } = null!;
        public virtual CartModel Cart { get; set; } = null!;
    }
    public partial class CategoryDTO
    {
        public CategoryDTO()
        {
            Subcategories = new HashSet<SubcategoryModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? PublicId { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual ICollection<SubcategoryModel> Subcategories { get; set; }
    }
    public partial class CoverTypeDTO
    {
        public CoverTypeDTO()
        {
            Books = new HashSet<BookModel>();
        }

        public int Id { get; set; }
        public string TypeName { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }

        public virtual ICollection<BookModel> Books { get; set; }
    }
    public partial class DimensionDTO
    {
        public DimensionDTO()
        {
            Books = new HashSet<BookModel>();
        }

        public int Id { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Unit { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }

        public virtual ICollection<BookModel> Books { get; set; }
    }
    public partial class FavouriteDTO
    {
        public int Id { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual BookModel Book { get; set; } = null!;
        public virtual AspNetUserModel User { get; set; } = null!;
    }
    public partial class FlashSaleDTO
    {
        public FlashSaleDTO()
        {
            FlashSaleBooks = new HashSet<FlashSaleBookModel>();
        }

        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual ICollection<FlashSaleBookModel> FlashSaleBooks { get; set; }
    }
    public partial class FlashSaleBookDTO
    {
        public int Id { get; set; }
        public int DiscountPercentage { get; set; }
        public int Quantity { get; set; }
        public int Sold { get; set; }
        public string? Poster { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual BookModel Book { get; set; } = null!;
        public virtual FlashSaleModel FlashSale { get; set; } = null!;
    }
    public partial class MenuDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Link { get; set; } = null!;
        public string? PublicId { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
    public partial class NotificationDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public bool IsRead { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual NotificationTypeModel NotificationType { get; set; } = null!;
        public virtual AspNetUserModel? User { get; set; }
    }
    public partial class NotificationTypeDTO
    {
        public NotificationTypeDTO()
        {
            Notifications = new HashSet<NotificationModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }

        public virtual ICollection<NotificationModel> Notifications { get; set; }
    }
    public partial class OrderDTO
    {
        public OrderDTO()
        {
            OrderItems = new HashSet<OrderItemDTO>();
            OrderStatuses = new HashSet<OrderStatusDTO>();
            Reviews = new HashSet<ReviewDTO>();
        }

        public int Id { get; set; }
        public string? Note { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual AddressModel Address { get; set; } = null!;
        public virtual PaymentMethodModel PaymentMethod { get; set; } = null!;
        public virtual AspNetUserModel User { get; set; } = null!;
        public virtual VoucherModel? Voucher { get; set; }
        public virtual PaymentModel? Payment { get; set; }
        public virtual ICollection<OrderItemDTO> OrderItems { get; set; }
        public virtual ICollection<OrderStatusDTO> OrderStatuses { get; set; }
        public virtual ICollection<ReviewDTO> Reviews { get; set; }
    }
    public partial class OrderItemDTO
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual BookDTO Book { get; set; } = null!;
        public virtual OrderModel Order { get; set; } = null!;
    }
    public partial class OrderStatusDTO
    {
        public int Id { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual OrderModel Order { get; set; } = null!;
        public virtual StatusModel Status { get; set; } = null!;
    }
    public partial class PartnerDTO
    {
        public PartnerDTO()
        {
            BookPartners = new HashSet<BookPartnerModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PublicId { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual PartnerTypeModel PartnerType { get; set; } = null!;
        public virtual ICollection<BookPartnerModel> BookPartners { get; set; }
    }
    public partial class PartnerTypeDTO
    {
        public PartnerTypeDTO()
        {
            Partners = new HashSet<PartnerModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }

        public virtual ICollection<PartnerModel> Partners { get; set; }
    }
    public partial class PaymentDTO
    {
        public int Id { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual OrderModel Order { get; set; } = null!;
    }
    public partial class PaymentMethodDTO
    {
        public PaymentMethodDTO()
        {
            Orders = new HashSet<OrderModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? PublicId { get; set; }
        public string? ImageUrl { get; set; }
        public bool Active { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual ICollection<OrderModel> Orders { get; set; }
    }
    public partial class PlatformDTO
    {
        public int Id { get; set; }
        public string PlatformName { get; set; } = null!;
        public string? PublicId { get; set; }
        public string? ImageUrl { get; set; }
        public string Link { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
    }
    public partial class PosterImageDTO
    {
        public int Id { get; set; }
        public string? PublicId { get; set; }
        public string? ImageUrl { get; set; }
        public bool ImageDefault { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual BookModel Book { get; set; } = null!;
    }
    public partial class ReviewDTO
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int OrderId { get; set; }
        public string UserId { get; set; } = null!;
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public bool Active { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual BookModel Book { get; set; } = null!;
        public virtual OrderModel Order { get; set; } = null!;
        public virtual AspNetUserModel User { get; set; } = null!;
    }
    public partial class StatusDTO
    {
        public StatusDTO()
        {
            OrderStatuses = new HashSet<OrderStatusModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }

        public virtual ICollection<OrderStatusModel> OrderStatuses { get; set; }
    }
    public partial class SubcategoryDTO
    {
        public SubcategoryDTO()
        {
            Books = new HashSet<BookModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? PublicId { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual CategoryModel Category { get; set; } = null!;
        public virtual ICollection<BookModel> Books { get; set; }
    }
    public partial class TopicDTO
    {
        public TopicDTO()
        {
            TopicContents = new HashSet<TopicContentModel>();
        }

        public int Id { get; set; }
        public string TopicName { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }

        public virtual ICollection<TopicContentModel> TopicContents { get; set; }
    }
    public partial class TopicContentDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }

        public virtual TopicModel Topic { get; set; } = null!;
    }
    public partial class VoucherDTO
    {
        public VoucherDTO()
        {
            Orders = new HashSet<OrderModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        public int DiscountPercent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MinOrderAmount { get; set; }
        public int MaxDiscountAmount { get; set; }
        public int UsageLimit { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual ICollection<OrderModel> Orders { get; set; }
    }
    public partial class WebsiteDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string LogoUrl { get; set; } = null!;
        public string IconUrl { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
    }
}
