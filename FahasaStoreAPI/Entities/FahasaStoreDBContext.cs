using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FahasaStoreAPI.Entities
{
    public partial class FahasaStoreDBContext : DbContext
    {
        public FahasaStoreDBContext()
        {
        }

        public FahasaStoreDBContext(DbContextOptions<FahasaStoreDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; } = null!;
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<Author> Authors { get; set; } = null!;
        public virtual DbSet<Banner> Banners { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<BookPartner> BookPartners { get; set; } = null!;
        public virtual DbSet<Cart> Carts { get; set; } = null!;
        public virtual DbSet<CartItem> CartItems { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<CoverType> CoverTypes { get; set; } = null!;
        public virtual DbSet<Dimension> Dimensions { get; set; } = null!;
        public virtual DbSet<Favourite> Favourites { get; set; } = null!;
        public virtual DbSet<FlashSale> FlashSales { get; set; } = null!;
        public virtual DbSet<FlashSaleBook> FlashSaleBooks { get; set; } = null!;
        public virtual DbSet<Menu> Menus { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<NotificationType> NotificationTypes { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderItem> OrderItems { get; set; } = null!;
        public virtual DbSet<OrderStatus> OrderStatuses { get; set; } = null!;
        public virtual DbSet<Partner> Partners { get; set; } = null!;
        public virtual DbSet<PartnerType> PartnerTypes { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; } = null!;
        public virtual DbSet<Platform> Platforms { get; set; } = null!;
        public virtual DbSet<PosterImage> PosterImages { get; set; } = null!;
        public virtual DbSet<Review> Reviews { get; set; } = null!;
        public virtual DbSet<Status> Statuses { get; set; } = null!;
        public virtual DbSet<Subcategory> Subcategories { get; set; } = null!;
        public virtual DbSet<Topic> Topics { get; set; } = null!;
        public virtual DbSet<TopicContent> TopicContents { get; set; } = null!;
        public virtual DbSet<Voucher> Vouchers { get; set; } = null!;
        public virtual DbSet<Website> Websites { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=FahasaStoreDB;Integrated Security=True;Encrypt=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("Address");

                entity.Property(e => e.AddressId).HasColumnName("address_id");

                entity.Property(e => e.DefaultAddress).HasColumnName("default_address");

                entity.Property(e => e.Detail)
                    .HasMaxLength(255)
                    .HasColumnName("detail");

                entity.Property(e => e.District)
                    .HasMaxLength(50)
                    .HasColumnName("district");

                entity.Property(e => e.Province)
                    .HasMaxLength(50)
                    .HasColumnName("province");

                entity.Property(e => e.ReceiverName)
                    .HasMaxLength(50)
                    .HasColumnName("receiver_name");

                entity.Property(e => e.UserId)
                    .HasMaxLength(450)
                    .HasColumnName("user_id");

                entity.Property(e => e.Ward)
                    .HasMaxLength(50)
                    .HasColumnName("ward");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Address__user_id__2DE6D218");
            });

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AspNetUserRoles");

                            j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                        });
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Author>(entity =>
            {
                entity.Property(e => e.AuthorId).HasColumnName("author_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Banner>(entity =>
            {
                entity.Property(e => e.BannerId).HasColumnName("banner_id");

                entity.Property(e => e.Content).HasColumnName("content");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.ImageUrl).HasColumnName("image_url");

                entity.Property(e => e.PublicId).HasColumnName("public_id");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.Property(e => e.AuthorId).HasColumnName("author_id");

                entity.Property(e => e.CoverTypeId).HasColumnName("cover_type_id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.DimensionId).HasColumnName("dimension_id");

                entity.Property(e => e.DiscountPercentage).HasColumnName("discount_percentage");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.PageCount).HasColumnName("page_count");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("price");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.SubcategoryId).HasColumnName("subcategory_id");

                entity.Property(e => e.Weight).HasColumnName("weight");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("FK__Books__author_id__0E6E26BF");

                entity.HasOne(d => d.CoverType)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.CoverTypeId)
                    .HasConstraintName("FK__Books__cover_typ__0F624AF8");

                entity.HasOne(d => d.Dimension)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.DimensionId)
                    .HasConstraintName("FK__Books__dimension__10566F31");

                entity.HasOne(d => d.Subcategory)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.SubcategoryId)
                    .HasConstraintName("FK__Books__subcatego__0D7A0286");
            });

            modelBuilder.Entity<BookPartner>(entity =>
            {
                entity.HasIndex(e => new { e.BookId, e.PartnerId }, "UQ__BookPart__2C7BEB527D8D9A99")
                    .IsUnique();

                entity.Property(e => e.BookPartnerId).HasColumnName("book_partner_id");

                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.Property(e => e.Note)
                    .HasMaxLength(50)
                    .HasColumnName("note");

                entity.Property(e => e.PartnerId).HasColumnName("partner_id");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.BookPartners)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK__BookPartn__book___18EBB532");

                entity.HasOne(d => d.Partner)
                    .WithMany(p => p.BookPartners)
                    .HasForeignKey(d => d.PartnerId)
                    .HasConstraintName("FK__BookPartn__partn__19DFD96B");
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasIndex(e => e.UserId, "UQ__Carts__B9BE370E9E122D68")
                    .IsUnique();

                entity.Property(e => e.CartId).HasColumnName("cart_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Cart)
                    .HasForeignKey<Cart>(d => d.UserId)
                    .HasConstraintName("FK__Carts__user_id__4C6B5938");
            });

            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.HasIndex(e => new { e.CartId, e.BookId }, "UQ__CartItem__2A65FB883DFC589B")
                    .IsUnique();

                entity.Property(e => e.CartItemId).HasColumnName("cart_item_id");

                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.Property(e => e.CartId).HasColumnName("cart_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.CartItems)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK__CartItems__book___51300E55");

                entity.HasOne(d => d.Cart)
                    .WithMany(p => p.CartItems)
                    .HasForeignKey(d => d.CartId)
                    .HasConstraintName("FK__CartItems__cart___503BEA1C");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasIndex(e => e.Name, "UQ__Categori__72E12F1BD5A323B6")
                    .IsUnique();

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.ImageUrl).HasColumnName("image_url");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.PublicId).HasColumnName("public_id");
            });

            modelBuilder.Entity<CoverType>(entity =>
            {
                entity.HasIndex(e => e.TypeName, "UQ__CoverTyp__543C4FD94263AD05")
                    .IsUnique();

                entity.Property(e => e.CoverTypeId).HasColumnName("cover_type_id");

                entity.Property(e => e.TypeName)
                    .HasMaxLength(50)
                    .HasColumnName("type_name");
            });

            modelBuilder.Entity<Dimension>(entity =>
            {
                entity.HasIndex(e => new { e.Length, e.Width, e.Height, e.Unit }, "UQ__Dimensio__FCE485DDE51E0712")
                    .IsUnique();

                entity.Property(e => e.DimensionId).HasColumnName("dimension_id");

                entity.Property(e => e.Height).HasColumnName("height");

                entity.Property(e => e.Length).HasColumnName("length");

                entity.Property(e => e.Unit)
                    .HasMaxLength(10)
                    .HasColumnName("unit");

                entity.Property(e => e.Width).HasColumnName("width");
            });

            modelBuilder.Entity<Favourite>(entity =>
            {
                entity.HasIndex(e => new { e.UserId, e.BookId }, "UQ__Favourit__BD2EE6A0A1745F24")
                    .IsUnique();

                entity.Property(e => e.FavouriteId).HasColumnName("favourite_id");

                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Favourites)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK__Favourite__book___5D95E53A");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Favourites)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Favourite__user___5CA1C101");
            });

            modelBuilder.Entity<FlashSale>(entity =>
            {
                entity.Property(e => e.FlashSaleId).HasColumnName("flash_sale_id");

                entity.Property(e => e.EndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("end_date");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("start_date");
            });

            modelBuilder.Entity<FlashSaleBook>(entity =>
            {
                entity.HasIndex(e => new { e.FlashSaleId, e.BookId }, "UQ__FlashSal__5124F239D45A83AD")
                    .IsUnique();

                entity.Property(e => e.FlashSaleBookId).HasColumnName("flash_sale_book_id");

                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.Property(e => e.DiscountPercentage).HasColumnName("discount_percentage");

                entity.Property(e => e.FlashSaleId).HasColumnName("flash_sale_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.FlashSaleBooks)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK__FlashSale__book___245D67DE");

                entity.HasOne(d => d.FlashSale)
                    .WithMany(p => p.FlashSaleBooks)
                    .HasForeignKey(d => d.FlashSaleId)
                    .HasConstraintName("FK__FlashSale__flash__236943A5");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("Menu");

                entity.HasIndex(e => e.Name, "UQ__Menu__72E12F1B1B8D1AA5")
                    .IsUnique();

                entity.Property(e => e.MenuId).HasColumnName("menu_id");

                entity.Property(e => e.ImageUrl).HasColumnName("image_url");

                entity.Property(e => e.Link).HasColumnName("link");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.PublicId).HasColumnName("public_id");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.Property(e => e.NotificationId).HasColumnName("notification_id");

                entity.Property(e => e.Content).HasColumnName("content");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.IsRead).HasColumnName("is_read");

                entity.Property(e => e.NotificationTypeId).HasColumnName("notification_type_id");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");

                entity.Property(e => e.UserId)
                    .HasMaxLength(450)
                    .HasColumnName("user_id");

                entity.HasOne(d => d.NotificationType)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.NotificationTypeId)
                    .HasConstraintName("FK__Notificat__notif__57DD0BE4");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Notificat__user___58D1301D");
            });

            modelBuilder.Entity<NotificationType>(entity =>
            {
                entity.HasIndex(e => e.Name, "UQ__Notifica__72E12F1B9725DABB")
                    .IsUnique();

                entity.Property(e => e.NotificationTypeId).HasColumnName("notification_type_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasIndex(e => new { e.UserId, e.VoucherId }, "UQ__Orders__21B558F40F8CEFFD")
                    .IsUnique();

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.AddressId).HasColumnName("address_id");

                entity.Property(e => e.Note)
                    .HasMaxLength(50)
                    .HasColumnName("note");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasColumnName("order_date");

                entity.Property(e => e.PaymentMethodId).HasColumnName("payment_method_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.VoucherId).HasColumnName("voucher_id");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK__Orders__address___395884C4");

                entity.HasOne(d => d.PaymentMethod)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.PaymentMethodId)
                    .HasConstraintName("FK__Orders__payment___3A4CA8FD");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Orders__user_id__37703C52");

                entity.HasOne(d => d.Voucher)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.VoucherId)
                    .HasConstraintName("FK__Orders__voucher___3864608B");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasIndex(e => new { e.OrderId, e.BookId }, "UQ__OrderIte__42C9B3864903A216")
                    .IsUnique();

                entity.Property(e => e.OrderItemId).HasColumnName("order_item_id");

                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK__OrderItem__book___3F115E1A");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__OrderItem__order__3E1D39E1");
            });

            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.ToTable("OrderStatus");

                entity.HasIndex(e => new { e.OrderId, e.StatusId }, "UQ__OrderSta__4531597B33ED7CFC")
                    .IsUnique();

                entity.Property(e => e.OrderStatusId).HasColumnName("order_status_id");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.OrderStatusDate)
                    .HasColumnType("datetime")
                    .HasColumnName("order_status_date");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderStatuses)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__OrderStat__order__43D61337");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.OrderStatuses)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK__OrderStat__statu__44CA3770");
            });

            modelBuilder.Entity<Partner>(entity =>
            {
                entity.Property(e => e.PartnerId).HasColumnName("partner_id");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .HasColumnName("address");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.ImageUrl).HasColumnName("image_url");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.PartnerTypeId).HasColumnName("partner_type_id");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .HasColumnName("phone");

                entity.Property(e => e.PublicId).HasColumnName("public_id");

                entity.HasOne(d => d.PartnerType)
                    .WithMany(p => p.Partners)
                    .HasForeignKey(d => d.PartnerTypeId)
                    .HasConstraintName("FK__Partners__partne__00200768");
            });

            modelBuilder.Entity<PartnerType>(entity =>
            {
                entity.HasIndex(e => e.Name, "UQ__PartnerT__72E12F1B9A30E6E5")
                    .IsUnique();

                entity.Property(e => e.PartnerTypeId).HasColumnName("partner_type_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasIndex(e => e.OrderId, "UQ__Payments__46596228BD18A864")
                    .IsUnique();

                entity.Property(e => e.PaymentId).HasColumnName("payment_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.HasOne(d => d.Order)
                    .WithOne(p => p.Payment)
                    .HasForeignKey<Payment>(d => d.OrderId)
                    .HasConstraintName("FK__Payments__order___489AC854");
            });

            modelBuilder.Entity<PaymentMethod>(entity =>
            {
                entity.HasIndex(e => e.Name, "UQ__PaymentM__72E12F1B205BA250")
                    .IsUnique();

                entity.Property(e => e.PaymentMethodId).HasColumnName("payment_method_id");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.ImageUrl).HasColumnName("image_url");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.PublicId).HasColumnName("public_id");
            });

            modelBuilder.Entity<Platform>(entity =>
            {
                entity.HasIndex(e => e.PlatformName, "UQ__Platform__139DBE443658D931")
                    .IsUnique();

                entity.Property(e => e.PlatformId).HasColumnName("platform_id");

                entity.Property(e => e.ImageUrl).HasColumnName("image_url");

                entity.Property(e => e.Link).HasColumnName("link");

                entity.Property(e => e.PlatformName)
                    .HasMaxLength(50)
                    .HasColumnName("platform_name");

                entity.Property(e => e.PublicId).HasColumnName("public_id");
            });

            modelBuilder.Entity<PosterImage>(entity =>
            {
                entity.Property(e => e.PosterImageId).HasColumnName("poster_image_id");

                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.Property(e => e.ImageDefault).HasColumnName("image_default");

                entity.Property(e => e.ImageUrl).HasColumnName("image_url");

                entity.Property(e => e.PublicId).HasColumnName("public_id");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.PosterImages)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK__PosterIma__book___1CBC4616");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasIndex(e => new { e.BookId, e.UserId }, "UQ__Reviews__A296F990C48F6345")
                    .IsUnique();

                entity.Property(e => e.ReviewId).HasColumnName("review_id");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.Property(e => e.Comment).HasColumnName("comment");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.ReviewDate)
                    .HasColumnType("datetime")
                    .HasColumnName("review_date");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK__Reviews__book_id__2A164134");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Reviews__user_id__2B0A656D");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("Status");

                entity.HasIndex(e => e.Name, "UQ__Status__72E12F1BCF05DF14")
                    .IsUnique();

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Subcategory>(entity =>
            {
                entity.HasIndex(e => e.Name, "UQ__Subcateg__72E12F1B3A1B8B25")
                    .IsUnique();

                entity.Property(e => e.SubcategoryId).HasColumnName("subcategory_id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.ImageUrl).HasColumnName("image_url");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.PublicId).HasColumnName("public_id");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Subcategories)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Subcatego__categ__7A672E12");
            });

            modelBuilder.Entity<Topic>(entity =>
            {
                entity.HasIndex(e => e.TopicName, "UQ__Topics__54BAE5EC45D0F960")
                    .IsUnique();

                entity.Property(e => e.TopicId).HasColumnName("topic_id");

                entity.Property(e => e.TopicName)
                    .HasMaxLength(255)
                    .HasColumnName("topic_name");
            });

            modelBuilder.Entity<TopicContent>(entity =>
            {
                entity.HasIndex(e => e.Title, "UQ__TopicCon__E52A1BB32DAA28A3")
                    .IsUnique();

                entity.Property(e => e.TopicContentId).HasColumnName("topic_content_id");

                entity.Property(e => e.Content).HasColumnName("content");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");

                entity.Property(e => e.TopicId).HasColumnName("topic_id");

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p.TopicContents)
                    .HasForeignKey(d => d.TopicId)
                    .HasConstraintName("FK__TopicCont__topic__70DDC3D8");
            });

            modelBuilder.Entity<Voucher>(entity =>
            {
                entity.HasIndex(e => e.Code, "UQ__Vouchers__357D4CF9148B7B52")
                    .IsUnique();

                entity.Property(e => e.VoucherId).HasColumnName("voucher_id");

                entity.Property(e => e.Code)
                    .HasMaxLength(100)
                    .HasColumnName("code");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.DiscountPercent).HasColumnName("discount_percent");

                entity.Property(e => e.EndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("end_date");

                entity.Property(e => e.MaxDiscountAmount)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("max_discount_amount");

                entity.Property(e => e.MinOrderAmount)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("min_order_amount");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("start_date");

                entity.Property(e => e.UsageLimit).HasColumnName("usage_limit");
            });

            modelBuilder.Entity<Website>(entity =>
            {
                entity.ToTable("Website");

                entity.Property(e => e.WebsiteId).HasColumnName("website_id");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .HasColumnName("address");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.IconUrl).HasColumnName("icon_url");

                entity.Property(e => e.LogoUrl).HasColumnName("logo_url");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .HasColumnName("phone");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
