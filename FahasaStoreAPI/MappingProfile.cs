using AutoMapper;
using FahasaStoreAPI.Entities;
using FahasaStoreAPI.Models.BasicModels;
using FahasaStoreAPI.Models.EntitiesModels;
using FahasaStoreAPI.Models.FormModels;
using System.Security.Principal;

namespace FahasaStoreAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Address, AddressForm>().ReverseMap();
            CreateMap<Author, AuthorForm>().ReverseMap();
            CreateMap<Banner, BannerForm>().ReverseMap();
            CreateMap<CoverType, CoverTypeForm>().ReverseMap();
            CreateMap<Book, BookForm>().ReverseMap();
            CreateMap<Cart, CartForm>().ReverseMap();
            CreateMap<CartItem, CartItemForm>().ReverseMap();
            CreateMap<Category, CategoryForm>().ReverseMap();
            CreateMap<Dimension, DimensionForm>().ReverseMap();
            CreateMap<FlashSale, FlashSaleForm>().ReverseMap();
            CreateMap<FlashSaleBook, FlashSaleBookForm>().ReverseMap();
            CreateMap<Help, HelpForm>().ReverseMap();
            CreateMap<HelpContent, HelpContentForm>().ReverseMap();
            CreateMap<Menu, MenuForm>().ReverseMap();
            CreateMap<Notification, NotificationForm>().ReverseMap();
            CreateMap<NotificationType, NotificationTypeForm>().ReverseMap();
            CreateMap<Order, OrderForm>().ReverseMap();
            CreateMap<OrderItem, OrderItemForm>().ReverseMap();
            CreateMap<OrderStatus, OrderStatusForm>().ReverseMap();
            CreateMap<Partner, PartnerForm>().ReverseMap();
            CreateMap<PartnerType, PartnerTypeForm>().ReverseMap();
            CreateMap<Payment, PaymentForm>().ReverseMap();
            CreateMap<PaymentMethod, PaymentMethodForm>().ReverseMap();
            CreateMap<PosterImage, PosterImageForm>().ReverseMap();
            CreateMap<Review, ReviewForm>().ReverseMap();
            CreateMap<Role, RoleForm>().ReverseMap();
            CreateMap<SocialMediaLink, SocialMediaLinkForm>().ReverseMap();
            CreateMap<Status, StatusForm>().ReverseMap();
            CreateMap<Subcategory, SubcategoryForm>().ReverseMap();
            CreateMap<User, UserForm>().ReverseMap();
            CreateMap<Voucher, VoucherForm>().ReverseMap();
            CreateMap<Website, WebsiteForm>().ReverseMap();

            CreateMap<Address, AddressBasic>().ReverseMap();
            CreateMap<Author, AuthorBasic>().ReverseMap();
            CreateMap<Banner, BannerBasic>().ReverseMap();
            CreateMap<CoverType, CoverTypeBasic>().ReverseMap();
            CreateMap<Book, BookBasic>().ReverseMap();
            CreateMap<Cart, CartBasic>().ReverseMap();
            CreateMap<CartItem, CartItemBasic>().ReverseMap();
            CreateMap<Category, CategoryBasic>().ReverseMap();
            CreateMap<Dimension, DimensionBasic>().ReverseMap();
            CreateMap<FlashSale, FlashSaleBasic>().ReverseMap();
            CreateMap<FlashSaleBook, FlashSaleBookBasic>().ReverseMap();
            CreateMap<Help, HelpBasic>().ReverseMap();
            CreateMap<HelpContent, HelpContentBasic>().ReverseMap();
            CreateMap<Menu, MenuBasic>().ReverseMap();
            CreateMap<Notification, NotificationBasic>().ReverseMap();
            CreateMap<NotificationType, NotificationTypeBasic>().ReverseMap();
            CreateMap<Order, OrderBasic>().ReverseMap();
            CreateMap<OrderItem, OrderItemBasic>().ReverseMap();
            CreateMap<OrderStatus, OrderStatusBasic>().ReverseMap();
            CreateMap<Partner, PartnerBasic>().ReverseMap();
            CreateMap<PartnerType, PartnerTypeBasic>().ReverseMap();
            CreateMap<Payment, PaymentBasic>().ReverseMap();
            CreateMap<PaymentMethod, PaymentMethodBasic>().ReverseMap();
            CreateMap<PosterImage, PosterImageBasic>().ReverseMap();
            CreateMap<Review, ReviewBasic>().ReverseMap();
            CreateMap<Role, RoleBasic>().ReverseMap();
            CreateMap<SocialMediaLink, SocialMediaLinkBasic>().ReverseMap();
            CreateMap<Status, StatusBasic>().ReverseMap();
            CreateMap<Subcategory, SubcategoryBasic>().ReverseMap();
            CreateMap<User, UserBasic>().ReverseMap();
            CreateMap<Voucher, VoucherBasic>().ReverseMap();
            CreateMap<Website, WebsiteBasic>().ReverseMap();

            CreateMap<Book, BookEntities>().ReverseMap();
        }
    }
}
