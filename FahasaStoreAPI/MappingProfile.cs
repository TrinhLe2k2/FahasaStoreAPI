using AutoMapper;
using FahasaStoreAPI.Entities;
using FahasaStoreAPI.Models;
using Microsoft.AspNetCore.Http;

namespace FahasaStoreAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
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
