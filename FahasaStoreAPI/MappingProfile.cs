using AutoMapper;
using FahasaStoreAPI.Entities;
using FahasaStoreAPI.Models;
using FahasaStoreAPI.Models.DTO;
using Microsoft.AspNetCore.Http;

namespace FahasaStoreAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Address, AddressDTO>().ReverseMap();
            CreateMap<Author, AuthorDTO>().ReverseMap();
            CreateMap<Banner, BannerDTO>().ReverseMap();
            CreateMap<Book, BookDTO>().ReverseMap();
            CreateMap<BookPartner, BookPartnerDTO>().ReverseMap();
            CreateMap<Cart, CartDTO>().ReverseMap();
            CreateMap<CartItem, CartItemDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Subcategory, SubcategoryDTO>().ReverseMap();
            CreateMap<CoverType, CoverTypeDTO>().ReverseMap();
            CreateMap<Dimension, DimensionDTO>().ReverseMap();
            CreateMap<Favourite, FavouriteDTO>().ReverseMap();
            CreateMap<FlashSale, FlashSaleDTO>().ReverseMap();
            CreateMap<FlashSaleBook, FlashSaleBookDTO>().ReverseMap();
            CreateMap<Menu, MenuDTO>().ReverseMap();
            CreateMap<Notification, NotificationDTO>().ReverseMap();
            CreateMap<NotificationType, NotificationTypeDTO>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<OrderItem, OrderItemDTO>().ReverseMap();
            CreateMap<OrderStatus, OrderStatusDTO>().ReverseMap();
            CreateMap<Partner, PartnerDTO>().ReverseMap();
            CreateMap<PartnerType, PartnerTypeDTO>().ReverseMap();
            CreateMap<Payment, PaymentDTO>().ReverseMap();
            CreateMap<PaymentMethod, PaymentMethodDTO>().ReverseMap();
            CreateMap<Platform, PlatformDTO>().ReverseMap();
            CreateMap<PosterImage, PosterImageDTO>().ReverseMap();
            CreateMap<Review, ReviewDTO>().ReverseMap();
            CreateMap<Status, StatusDTO>().ReverseMap();
            CreateMap<Topic, TopicDTO>().ReverseMap();
            CreateMap<TopicContent, TopicContentDTO>().ReverseMap();
            CreateMap<Voucher, VoucherDTO>().ReverseMap();
            CreateMap<Website, WebsiteDTO>().ReverseMap();
            CreateMap<AspNetUser, AspNetUserDTO>().ReverseMap();
            CreateMap<AspNetRole, AspNetRoleDTO>().ReverseMap();

            CreateMap<Address, AddressModel>().ReverseMap();
            CreateMap<Author, AuthorModel>().ReverseMap();
            CreateMap<Banner, BannerModel>().ReverseMap();
            CreateMap<Book, BookModel>().ReverseMap();
            CreateMap<BookPartner, BookPartnerModel>().ReverseMap();
            CreateMap<Cart, CartModel>().ReverseMap();
            CreateMap<CartItem, CartItemModel>().ReverseMap();
            CreateMap<Category, CategoryModel>().ReverseMap();
            CreateMap<Subcategory, SubcategoryModel>().ReverseMap();
            CreateMap<CoverType, CoverTypeModel>().ReverseMap();
            CreateMap<Dimension, DimensionModel>().ReverseMap();
            CreateMap<Favourite, FavouriteModel>().ReverseMap();
            CreateMap<FlashSale, FlashSaleModel>().ReverseMap();
            CreateMap<FlashSaleBook, FlashSaleBookModel>().ReverseMap();
            CreateMap<Menu, MenuModel>().ReverseMap();
            CreateMap<Notification, NotificationModel>().ReverseMap();
            CreateMap<NotificationType, NotificationTypeModel>().ReverseMap();
            CreateMap<Order, OrderModel>().ReverseMap();
            CreateMap<OrderItem, OrderItemModel>().ReverseMap();
            CreateMap<OrderStatus, OrderStatusModel>().ReverseMap();
            CreateMap<Partner, PartnerModel>().ReverseMap();
            CreateMap<PartnerType, PartnerTypeModel>().ReverseMap();
            CreateMap<Payment, PaymentModel>().ReverseMap();
            CreateMap<PaymentMethod, PaymentMethodModel>().ReverseMap();
            CreateMap<Platform, PlatformModel>().ReverseMap();
            CreateMap<PosterImage, PosterImageModel>().ReverseMap();
            CreateMap<Review, ReviewModel>().ReverseMap();
            CreateMap<Status, StatusModel>().ReverseMap();
            CreateMap<Topic, TopicModel>().ReverseMap();
            CreateMap<TopicContent, TopicContentModel>().ReverseMap();
            CreateMap<Voucher, VoucherModel>().ReverseMap();
            CreateMap<Website, WebsiteModel>().ReverseMap();
            CreateMap<AspNetUser, AspNetUserModel>().ReverseMap();
            CreateMap<AspNetRole, AspNetRoleModel>().ReverseMap();
        }
    }
}
