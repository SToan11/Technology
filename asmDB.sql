create database QuanLiBH1;
go

use QuanLiBH1;
go


create table Categories (
    CategoryID int primary key identity(1,1),
    CategoryName nvarchar(100) not null,
    Description nvarchar(255),
	Status int default 1 -- 1 là hoạt động, 0 là đã xóa
);
go

create table Product(
	ProductId int identity(1,1),
	Name nvarchar(50),
	Category nvarchar(50),
	Image nvarchar(255),
	Description nvarchar(255),
	Color nvarchar(50),
	UnitPrice float,
	AvailableQuantity int
);

INSERT INTO Product (Name, Category, Image, Description, Color, UnitPrice, AvailableQuantity)
VALUES 
('MacBook Pro 16-inch', 'Laptop', 'macbookpro16.png', 'Powerful laptop with M1 chip', 'Space Gray', 2399.00, 15),
('HP Spectre x360', 'Laptop', 'hpspectre.png', 'Convertible laptop with 4K touchscreen', 'Nightfall Black', 1599.99, 20),
('Microsoft Surface Laptop 4', 'Laptop', 'surfacelaptop4.png', 'Ultra-thin laptop with AMD Ryzen processor', 'Matte Black', 1299.00, 18),
('Lenovo ThinkPad X1 Carbon', 'Laptop', 'thinkpadx1carbon.png', 'Business laptop with lightweight design', 'Black', 1799.00, 10),
('ASUS ZenBook 14', 'Laptop', 'zenbook14.png', 'Ultrabook with NanoEdge display', 'Pine Gray', 999.99, 25),
('Dell Inspiron 15', 'Laptop', 'inspiron15.png', 'Affordable laptop with 15.6-inch screen', 'Silver', 699.99, 30),
('Google Pixel 6 Pro', 'Smartphone', 'pixel6pro.png', 'Flagship smartphone with Tensor chip', 'Stormy Black', 899.00, 50),
('Samsung Galaxy Z Fold 3', 'Smartphone', 'galaxyzfold3.png', 'Foldable smartphone with 7.6-inch display', 'Phantom Black', 1799.00, 12),
('Apple iPhone 13 Pro Max', 'Smartphone', 'iphone13promax.png', '6.7-inch display with ProMotion technology', 'Graphite', 1199.00, 45),
('OnePlus 9 Pro', 'Smartphone', 'oneplus9pro.png', '5G smartphone with Hasselblad camera', 'Morning Mist', 1069.00, 40),
('Huawei MateBook X Pro', 'Laptop', 'matebookxpro.png', 'Ultra-slim laptop with 3K touch display', 'Emerald Green', 1499.00, 22),
('Sony Xperia 1 III', 'Smartphone', 'xperia1iii.png', 'Flagship smartphone with 4K HDR display', 'Frosted Black', 1299.99, 35),
('Acer Predator Helios 300', 'Laptop', 'predatorhelios300.png', 'Gaming laptop with 144Hz display', 'Abyssal Black', 1299.00, 18),
('MSI GE76 Raider', 'Laptop', 'ge76raider.png', 'High-performance gaming laptop with RTX 3080', 'Titanium Blue', 2999.00, 8),
('Razer Blade 15', 'Laptop', 'razerblade15.png', 'Gaming laptop with OLED 4K display', 'Black', 2399.99, 12);



create table Customers (
    CustomerID int primary key identity(1,1),
    Email nvarchar(100) not null unique,
    Password nvarchar(100) not null,
);
go

select * from Customers

INSERT INTO Customers (Email, Password) VALUES ('toan@gmail.com','123')

create table Carts (
    CartID int primary key identity(1,1),
    CustomerID int,
    TotalAmount float not null,
    foreign key (CustomerID) references Customers(CustomerID)
);
go

create table Cart_detail (
    CartDetailID int primary key identity(1,1),
    CartID int,
    ProductID int,
    ProductName nvarchar(100),
    Quantity int,
    Price float,
    ProductDescription nvarchar(255),
    foreign key (CartID) references Carts(CartID),
    foreign key (ProductID) references Products(ProductID)
);
go

create table Account (
    Id int primary key identity(1,1),
    Name nvarchar(100) not null,
    Email nvarchar(100) unique not null,
    Password nvarchar(255) not null, 
    Role varchar(50) not null,
    CreateDate datetime not null,
);
go
select * from Account


insert into Account(Name, Email, Password, Role, CreateDate) 
values
('toan','toan1@gmail.com','1111','User','29/1/2002');
('SongToan','toan@gmail.com','1111','Admin', '1/11/2005')


drop table Account



