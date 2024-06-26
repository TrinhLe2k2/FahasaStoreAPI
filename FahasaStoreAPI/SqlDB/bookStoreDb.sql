﻿    
-- CREATE DATABASE FahasaStoreDB;
-- USE FahasaStoreDB

-- Tạo bảng Website
CREATE TABLE Website (
    website_id INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(100) NOT NULL,
    logo_url NVARCHAR(MAX) NOT NULL,
    icon_url NVARCHAR(MAX) NOT NULL,
    description NVARCHAR(255) NOT NULL,
    address NVARCHAR(255) NOT NULL,
    phone NVARCHAR(20) NOT NULL,
    email NVARCHAR(100) NOT NULL
);

-- Tạo bảng Banners
CREATE TABLE Banners (
    banner_id INT PRIMARY KEY IDENTITY(1,1),
	public_id NVARCHAR(MAX),
    image_url NVARCHAR(MAX) NOT NULL,
    title NVARCHAR(255) NOT NULL,
    content NVARCHAR(MAX) NOT NULL,
    created_at DATETIME NOT NULL
);

-- Tạo bảng Menu
CREATE TABLE Menu (
    menu_id INT PRIMARY KEY IDENTITY(1,1),
	name NVARCHAR(255) NOT NULL UNIQUE,
    link NVARCHAR(MAX) NOT NULL,
    public_id NVARCHAR(MAX),
    image_url NVARCHAR(MAX) NOT NULL
);

CREATE TABLE Vouchers (
    voucher_id INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(255) NOT NULL,
    code NVARCHAR(100) NOT NULL UNIQUE,
    description NVARCHAR(MAX),
    discount_percent INT NOT NULL CHECK (discount_percent >= 0 AND discount_percent <= 100),
    start_date DATETIME NOT NULL,
    end_date DATETIME NOT NULL,
    min_order_amount DECIMAL(10,2) NOT NULL CHECK (min_order_amount >= 0),
    max_discount_amount DECIMAL(10,2) NOT NULL CHECK (max_discount_amount >= 0),
    usage_limit INT NOT NULL CHECK (usage_limit >= 0),
	CHECK (start_date <= end_date)
);


-- Tạo bảng Topics
CREATE TABLE Topics (
    topic_id INT PRIMARY KEY IDENTITY(1,1),
    topic_name NVARCHAR(255) NOT NULL UNIQUE
);

-- Tạo bảng HelpContents
CREATE TABLE TopicContents (
    topic_content_id INT PRIMARY KEY IDENTITY(1,1),
    topic_id INT FOREIGN KEY REFERENCES Topics(topic_id),
    title NVARCHAR(255) NOT NULL UNIQUE,
    content NVARCHAR(MAX) NOT NULL
);

-- Tạo bảng platforms
CREATE TABLE Platforms (
    platform_id INT PRIMARY KEY IDENTITY(1,1),
    platform_name NVARCHAR(50) NOT NULL UNIQUE,
	public_id NVARCHAR(MAX),
    image_url NVARCHAR(MAX) NOT NULL,
    link NVARCHAR(MAX) NOT NULL
);

-- Tạo bảng Categories
CREATE TABLE Categories (
    category_id INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(50) NOT NULL UNIQUE,
	public_id NVARCHAR(MAX),
    image_url NVARCHAR(MAX) NOT NULL
);

-- Tạo bảng Subcategories
CREATE TABLE Subcategories (
    subcategory_id INT PRIMARY KEY IDENTITY(1,1),
    category_id INT FOREIGN KEY REFERENCES Categories(category_id),
    name NVARCHAR(50) NOT NULL UNIQUE,
	public_id NVARCHAR(MAX),
    image_url NVARCHAR(MAX) NOT NULL
);

-- Tạo bảng PartnerTypes
CREATE TABLE PartnerTypes (
    partner_type_id INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(100) NOT NULL UNIQUE
)

-- Tạo bảng Partners
CREATE TABLE Partners (
    partner_id INT PRIMARY KEY IDENTITY(1,1),
	partner_type_id INT FOREIGN KEY REFERENCES PartnerTypes(partner_type_id),
    name NVARCHAR(100) NOT NULL,
    address NVARCHAR(255) NOT NULL,
    phone NVARCHAR(20) NOT NULL,
    email NVARCHAR(100) NOT NULL,
	public_id NVARCHAR(MAX),
	image_url NVARCHAR(MAX) NOT NULL
);

-- Tạo bảng Authors
CREATE TABLE Authors (
    author_id INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(100) NOT NULL
);

-- Tạo bảng CoverTypes
CREATE TABLE CoverTypes (
    cover_type_id INT PRIMARY KEY IDENTITY(1,1),
    type_name NVARCHAR(50) NOT NULL UNIQUE
);

-- Tạo bảng Dimensions
CREATE TABLE Dimensions (
    dimension_id INT PRIMARY KEY IDENTITY(1,1),
    length FLOAT NOT NULL CHECK (length > 0),
    width FLOAT NOT NULL CHECK (width > 0),
    height FLOAT NOT NULL CHECK (height > 0),
	unit NVARCHAR(10) NOT NULL,
	UNIQUE (length, width, height, unit)
);

-- Tạo bảng Books
CREATE TABLE Books (
    book_id INT PRIMARY KEY IDENTITY(1,1),
    subcategory_id INT FOREIGN KEY REFERENCES Subcategories(subcategory_id),
    author_id INT FOREIGN KEY REFERENCES Authors(author_id),
    cover_type_id INT FOREIGN KEY REFERENCES CoverTypes(cover_type_id),
    dimension_id INT FOREIGN KEY REFERENCES Dimensions(dimension_id),
    name NVARCHAR(255) NOT NULL,
    description NVARCHAR(MAX) NOT NULL,
    price DECIMAL(10,2) NOT NULL CHECK (price >= 0),
    discount_percentage INT NOT NULL CHECK (discount_percentage >= 0 AND discount_percentage <= 100),
    quantity INT NOT NULL CHECK (quantity >= 0),
    weight FLOAT CHECK (weight > 0),
    page_count INT CHECK (page_count > 0)
);

-- Tạo bảng BookPartners
CREATE TABLE BookPartners (
	book_partner_id INT PRIMARY KEY IDENTITY(1,1),
	book_id INT FOREIGN KEY REFERENCES Books(book_id),
	partner_id INT FOREIGN KEY REFERENCES Partners(partner_id),
    note NVARCHAR(50),
	UNIQUE (book_id, partner_id)
);

-- Tạo bảng PosterImages
CREATE TABLE PosterImages (
    poster_image_id INT PRIMARY KEY IDENTITY(1,1),
	book_id INT FOREIGN KEY REFERENCES Books(book_id),
	public_id NVARCHAR(MAX),
    image_url NVARCHAR(MAX) NOT NULL,
	image_default BIT NOT NULL,
);

-- Tạo bảng FlashSales
CREATE TABLE FlashSales (
    flash_sale_id INT PRIMARY KEY IDENTITY(1,1),
    start_date DATETIME NOT NULL,
    end_date DATETIME NOT NULL,
	CHECK (end_date > start_date)
);

CREATE TABLE FlashSaleBooks (
	flash_sale_book_id INT PRIMARY KEY IDENTITY(1,1),
    flash_sale_id INT FOREIGN KEY REFERENCES FlashSales(flash_sale_id),
    book_id INT FOREIGN KEY REFERENCES Books(book_id),
    discount_percentage INT NOT NULL CHECK (discount_percentage >= 0 AND discount_percentage <= 100),
	quantity INT NOT NULL CHECK (quantity >= 0),
	UNIQUE (flash_sale_id, book_id),
);

-- Tạo bảng Reviews
CREATE TABLE Reviews (
	review_id INT PRIMARY KEY IDENTITY(1,1),
    book_id INT FOREIGN KEY REFERENCES Books(book_id),
    user_id NVARCHAR(450) FOREIGN KEY REFERENCES AspNetUsers(id),
    rating INT NOT NULL,
    comment NVARCHAR(MAX),
    review_date DATETIME NOT NULL,
	active BIT NOT NULL,
	UNIQUE (book_id, user_id),
);

-- Tạo bảng Address
CREATE TABLE Address (
    address_id INT PRIMARY KEY IDENTITY(1,1),
    user_id NVARCHAR(450) FOREIGN KEY REFERENCES AspNetUsers(id),
	receiver_name NVARCHAR(50) NOT NULL,
    province NVARCHAR(50) NOT NULL,
    district NVARCHAR(50) NOT NULL,
    ward NVARCHAR(50) NOT NULL,
    detail NVARCHAR(255) NOT NULL,
    default_address BIT NOT NULL
);

-- Tạo bảng PaymentMethods
CREATE TABLE PaymentMethods (
    payment_method_id INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(50) NOT NULL UNIQUE,
	public_id NVARCHAR(MAX),
    image_url NVARCHAR(MAX) NOT NULL,
	active BIT NOT NULL
);

-- Tạo bảng Status
CREATE TABLE Status (
    status_id INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(50) NOT NULL UNIQUE,
);

-- Tạo bảng Orders
CREATE TABLE Orders (
    order_id INT PRIMARY KEY IDENTITY(1,1),
    user_id NVARCHAR(450) FOREIGN KEY REFERENCES AspNetUsers(id),
	voucher_id INT FOREIGN KEY REFERENCES Vouchers(voucher_id),
	address_id INT FOREIGN KEY REFERENCES Address(address_id),
	payment_method_id INT FOREIGN KEY REFERENCES PaymentMethods(payment_method_id),
    order_date DATETIME NOT NULL,
	note NVARCHAR(50),
	UNIQUE (user_id, voucher_id)
);

-- Tạo bảng OrderItems
CREATE TABLE OrderItems (
	order_item_id INT PRIMARY KEY IDENTITY(1,1),
    order_id INT FOREIGN KEY REFERENCES Orders(order_id),
    book_id INT FOREIGN KEY REFERENCES Books(book_id),
    quantity INT NOT NULL CHECK (quantity > 0),
	UNIQUE (order_id, book_id),
);

-- Tạo bảng OrderStatus
CREATE TABLE OrderStatus (
	order_status_id INT PRIMARY KEY IDENTITY(1,1),
    order_id INT FOREIGN KEY REFERENCES Orders(order_id),
    status_id INT FOREIGN KEY REFERENCES Status(status_id),
    order_status_date DATETIME NOT NULL,
	UNIQUE (order_id, status_id),
);

-- Tạo bảng Payments
CREATE TABLE Payments (
	payment_id INT PRIMARY KEY IDENTITY(1,1),
    order_id INT UNIQUE FOREIGN KEY REFERENCES Orders(order_id),
	created_at DATETIME NOT NULL
);

-- Tạo bảng Carts
CREATE TABLE Carts (
    cart_id INT PRIMARY KEY IDENTITY(1,1),
    user_id NVARCHAR(450) UNIQUE FOREIGN KEY REFERENCES AspNetUsers(id),
    created_at DATETIME NOT NULL
);

-- Tạo bảng CartItems
CREATE TABLE CartItems (
	cart_item_id INT PRIMARY KEY IDENTITY(1,1),
    cart_id INT FOREIGN KEY REFERENCES Carts(cart_id),
    book_id INT FOREIGN KEY REFERENCES Books(book_id),
    quantity INT CHECK (quantity > 0),
	UNIQUE (cart_id, book_id)
);

CREATE TABLE NotificationTypes (
    notification_type_id INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(255) NOT NULL UNIQUE
);

CREATE TABLE Notifications (
    notification_id INT PRIMARY KEY IDENTITY(1,1),
	notification_type_id INT FOREIGN KEY REFERENCES NotificationTypes(notification_type_id),
	user_id NVARCHAR(450) FOREIGN KEY REFERENCES AspNetUsers(id),
    title NVARCHAR(255) NOT NULL,
    content NVARCHAR(MAX) NOT NULL,
    created_at DATETIME NOT NULL,
	is_read BIT NOT NULL
);

-- Tạo bảng Favourites
CREATE TABLE Favourites (
	favourite_id INT PRIMARY KEY IDENTITY(1,1),
    user_id NVARCHAR(450) FOREIGN KEY REFERENCES AspNetUsers(id),
    book_id INT FOREIGN KEY REFERENCES Books(book_id),
    created_at DATETIME NOT NULL,
	UNIQUE (user_id, book_id)
);