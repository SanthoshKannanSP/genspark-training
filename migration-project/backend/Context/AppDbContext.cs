using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Color> Colors { get; set; }
    public DbSet<Model> Models { get; set; }
    public DbSet<News> News { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<ContactUs> ContactUs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderDetail>().HasKey(od => new { od.OrderID, od.ProductID });

        modelBuilder.Entity<Category>().HasData(
            new Category { CategoryId = 1, Name = "Iphone" },
            new Category { CategoryId = 2, Name = "Ipad" },
            new Category { CategoryId = 3, Name = "Macbook" }
        );

        modelBuilder.Entity<Color>().HasData(
        new Color { ColorId = 1, Name = "Rose" },
        new Color { ColorId = 2, Name = "Gold" },
        new Color { ColorId = 3, Name = "White" },
        new Color { ColorId = 4, Name = "Black" },
        new Color { ColorId = 5, Name = "Grey" }
        );

        modelBuilder.Entity<ContactUs>().HasData(
        new ContactUs
        {
            Id = 1,
            Name = "Chien Vu",
            Email = "chien.vh@gmail.com",
            Phone = "986665248",
            Content = "What the hell?"
        },
        new ContactUs
        {
            Id = 2,
            Name = "Chien Vu Hoang",
            Email = "chien.vh@gmail.com",
            Phone = "986665248",
            Content = "2nd time for submitting form. thanks"
        }
        );

        modelBuilder.Entity<Model>().HasData(
            new Model { ModelId = 1, Name = "6S Rose Gold" },
            new Model { ModelId = 2, Name = "6S Gold" },
            new Model { ModelId = 3, Name = "6 Gold" },
            new Model { ModelId = 4, Name = "6 Grey" },
            new Model { ModelId = 5, Name = "6S Rose Plus" },
            new Model { ModelId = 6, Name = "6S Gold Plus" },
            new Model { ModelId = 7, Name = "6 Gold Plus" },
            new Model { ModelId = 8, Name = "6 Grey Plus" },
            new Model { ModelId = 9, Name = "5S Gold" },
            new Model { ModelId = 10, Name = "5S Black" },
            new Model { ModelId = 11, Name = "5S White" },
            new Model { ModelId = 12, Name = "5 White" },
            new Model { ModelId = 13, Name = "5 Black" },
            new Model { ModelId = 14, Name = "Ipad Mini 1" },
            new Model { ModelId = 15, Name = "Ipad Mini 2" },
            new Model { ModelId = 16, Name = "Ipad Mini 3" },
            new Model { ModelId = 17, Name = "Ipad Mini 4" },
            new Model { ModelId = 18, Name = "Ipad 2" },
            new Model { ModelId = 19, Name = "Ipad 3" },
            new Model { ModelId = 20, Name = "Ipad 4" },
            new Model { ModelId = 21, Name = "Ipad Air" },
            new Model { ModelId = 22, Name = "Macbook Pro" },
            new Model { ModelId = 23, Name = "Macbook Pro Retina" }
        );

        var createdDate = new DateTime(2015, 11, 11).ToUniversalTime();
        modelBuilder.Entity<News>().HasData(
        new News { NewsId = 2, UserId = 1, Title = "My Testing First", ShortDescription = "This is the short description first", Image = "iPhone 6 Plus 16Gb Grey.jpg", Content = "Content should be here. Will be updated later", CreatedDate = createdDate, Status = 1 },
        new News { NewsId = 3, UserId = 1, Title = "My Testing 2", ShortDescription = "This is the short description 2", Image = "iPhone 5S 32GB Silver.jpg", Content = "Content should be here. Will be updated later", CreatedDate = createdDate, Status = 1 },
        new News { NewsId = 4, UserId = 1, Title = "My Testing 3", ShortDescription = "This is the short description 3", Image = "iPhone 6 Plus 16Gb Grey.jpg", Content = "Content should be here. Will be updated later", CreatedDate = createdDate, Status = 1 },
        new News { NewsId = 5, UserId = 1, Title = "My Testing 4", ShortDescription = "This is the short description 4", Image = "iPhone 5S 32GB Silver.jpg", Content = "Content should be here. Will be updated later", CreatedDate = createdDate, Status = 1 },
        new News { NewsId = 6, UserId = 1, Title = "My Testing 5", ShortDescription = "This is the short description 5", Image = "iPhone 6 Plus 16Gb Grey.jpg", Content = "Content should be here. Will be updated later", CreatedDate = createdDate, Status = 1 },
        new News { NewsId = 7, UserId = 1, Title = "My Testing 6", ShortDescription = "This is the short description 6", Image = "iPhone 5S 32GB Silver.jpg", Content = "Content should be here. Will be updated later", CreatedDate = createdDate, Status = 1 },
        new News { NewsId = 8, UserId = 1, Title = "My Testing 7", ShortDescription = "This is the short description 7", Image = "iPhone 5S 32GB Silver.jpg", Content = "Content should be here. Will be updated later", CreatedDate = createdDate, Status = 1 },
        new News { NewsId = 9, UserId = 1, Title = "My Testing 8", ShortDescription = "This is the short description 8", Image = "iPhone 5S 32GB Silver.jpg", Content = "Content should be here. Will be updated later", CreatedDate = createdDate, Status = 1 },
        new News { NewsId = 10, UserId = 1, Title = "My Testing 9", ShortDescription = "This is the short description 9", Image = "iPhone 6 Plus 16Gb Grey.jpg", Content = "Content should be here. Will be updated later", CreatedDate = createdDate, Status = 1 },
        new News { NewsId = 11, UserId = 1, Title = "My Testing 10", ShortDescription = "This is the short description 10", Image = "iPhone 5S 32GB Silver.jpg", Content = "Content should be here. Will be updated later", CreatedDate = createdDate, Status = 1 },
        new News { NewsId = 12, UserId = 1, Title = "My Testing 11", ShortDescription = "This is the short description 11", Image = "iPhone 6 Plus 16Gb Grey.jpg", Content = "Content should be here. Will be updated later", CreatedDate = createdDate, Status = 1 },
        new News { NewsId = 13, UserId = 1, Title = "My Testing 12", ShortDescription = "This is the short description 12", Image = "iPhone 5S 32GB Silver.jpg", Content = "Content should be here. Will be updated later", CreatedDate = createdDate, Status = 1 },
        new News { NewsId = 14, UserId = 1, Title = "My Testing 13", ShortDescription = "This is the short description 13", Image = "iPhone 5S 32GB Silver.jpg", Content = "Content should be here. Will be updated later", CreatedDate = createdDate, Status = 1 },
        new News { NewsId = 15, UserId = 1, Title = "My Testing 14", ShortDescription = "This is the short description 14", Image = "iPhone 6 Plus 16Gb Grey.jpg", Content = "Content should be here. Will be updated later", CreatedDate = createdDate, Status = 1 },
        new News { NewsId = 16, UserId = 1, Title = "My Testing 15", ShortDescription = "This is the short description 15", Image = "iPhone 6 Plus 16Gb Grey.jpg", Content = "Content should be here. Will be updated later", CreatedDate = createdDate, Status = 1 },
        new News { NewsId = 17, UserId = 1, Title = "My Testing 16", ShortDescription = "This is the short description 16", Image = "iPhone 6 Plus 16Gb Grey.jpg", Content = "Content should be here. Will be updated later", CreatedDate = createdDate, Status = 1 },
        new News { NewsId = 18, UserId = 1, Title = "My Testing 17", ShortDescription = "This is the short description 17", Image = "iPhone 5S 32GB Silver.jpg", Content = "Content should be here. Will be updated later", CreatedDate = createdDate, Status = 1 },
        new News { NewsId = 19, UserId = 1, Title = "My Testing 18", ShortDescription = "This is the short description 18", Image = "iPhone 5S 32GB Silver.jpg", Content = "Content should be here. Will be updated later", CreatedDate = createdDate, Status = 1 },
        new News { NewsId = 20, UserId = 1, Title = "My Testing 19", ShortDescription = "This is the short description 19", Image = "iPhone 6 Plus 16Gb Grey.jpg", Content = "Content should be here. Will be updated later", CreatedDate = createdDate, Status = 1 },
        new News { NewsId = 21, UserId = 1, Title = "My Testing 20", ShortDescription = "This is the short description 20", Image = "iPhone 6 Plus 16Gb Grey.jpg", Content = "Content should be here. Will be updated later", CreatedDate = createdDate, Status = 1 },
        new News { NewsId = 22, UserId = 1, Title = "My Testing 21", ShortDescription = "This is the short description 21", Image = "iPhone 6 Plus 16Gb Grey.jpg", Content = "Content should be here. Will be updated later", CreatedDate = createdDate, Status = 1 },
        new News { NewsId = 23, UserId = 1, Title = "My Testing 22", ShortDescription = "This is the short description 22", Image = "iPhone 5S 32GB Silver.jpg", Content = "Content should be here. Will be updated later", CreatedDate = createdDate, Status = 1 }
        );

        modelBuilder.Entity<Order>().HasData(
    new Order
    {
        OrderID = 1,
        OrderName = "Order 1",
        OrderDate = new DateTime(2017, 7, 2).ToUniversalTime(),
        PaymentType = "Cash",
        Status = "Paid",
        CustomerName = "Chien Vu",
        CustomerPhone = "84986665248",
        CustomerEmail = "chien.vh@gmail.com",
        CustomerAddress = "My Dinh, Ha Noi, Viet Nam"
    },
    new Order
    {
        OrderID = 2,
        OrderName = "Order 2",
        OrderDate = new DateTime(2017, 7, 7).ToUniversalTime(),
        PaymentType = "Cash",
        Status = "Processing",
        CustomerName = "Chien Vu Hoang",
        CustomerPhone = "84986665248",
        CustomerEmail = "chien.vh@hotmail.com",
        CustomerAddress = "Ha Noi, Viet Nam"
    },
    new Order
    {
        OrderID = 3,
        OrderName = "Order 3",
        OrderDate = new DateTime(2017, 8, 26).ToUniversalTime(),
        PaymentType = "Cash",
        Status = "Processing",
        CustomerName = "Hoang Hop",
        CustomerPhone = "986665248",
        CustomerEmail = "chien.vh@gmail.com",
        CustomerAddress = "My Dinh, Ha Noi, Viet Nam"
    },
    new Order
    {
        OrderID = 4,
        OrderName = "Order 4",
        OrderDate = new DateTime(2017, 8, 26).ToUniversalTime(),
        PaymentType = "Cash",
        Status = "Processing",
        CustomerName = "Hop Hoang",
        CustomerPhone = "986665248",
        CustomerEmail = "chien.vh@gmail.com",
        CustomerAddress = "My Dinh, Ha Noi, Viet Nam"
    },
    new Order
    {
        OrderID = 5,
        OrderName = "Order 5",
        OrderDate = new DateTime(2017, 8, 26).ToUniversalTime(),
        PaymentType = "Cash",
        Status = "Processing",
        CustomerName = "Chien VH",
        CustomerPhone = "986665248",
        CustomerEmail = "chien.vh@gmail.com",
        CustomerAddress = "My Dinh, Ha Noi, Viet Nam"
    },
    new Order
    {
        OrderID = 6,
        OrderName = "Order 6",
        OrderDate = new DateTime(2017, 8, 26).ToUniversalTime(),
        PaymentType = "Cash",
        Status = "Processing",
        CustomerName = "Chien",
        CustomerPhone = "986665248",
        CustomerEmail = "chien.vh@gmail.com",
        CustomerAddress = "My Dinh, Ha Noi, Viet Nam"
    },
    new Order
    {
        OrderID = 7,
        OrderName = "Order 7",
        OrderDate = new DateTime(2017, 8, 26).ToUniversalTime(),
        PaymentType = "Cash",
        Status = "Processing",
        CustomerName = "Chien Hoang",
        CustomerPhone = "986665248",
        CustomerEmail = "chien.vh@gmail.com",
        CustomerAddress = "My Dinh, Ha Noi, Viet Nam"
    },
    new Order
    {
        OrderID = 8,
        OrderName = "Order 8",
        OrderDate = new DateTime(2017, 8, 27).ToUniversalTime(),
        PaymentType = "Cash",
        Status = "Processing",
        CustomerName = "Hop HT",
        CustomerPhone = "986665248",
        CustomerEmail = "chien.vh@gmail.com",
        CustomerAddress = "Ha Noi, Viet Nam"
    },
    new Order
    {
        OrderID = 9,
        OrderName = "Order 9",
        OrderDate = new DateTime(2017, 8, 27).ToUniversalTime(),
        PaymentType = "Cash",
        Status = "Processing",
        CustomerName = "Chien Vu",
        CustomerPhone = "986665248",
        CustomerEmail = "chien.vh@gmail.com",
        CustomerAddress = "Ha Noi, Viet Nam"
    },
    new Order
    {
        OrderID = 10,
        OrderName = "Order 10",
        OrderDate = new DateTime(2017, 9, 3).ToUniversalTime(),
        PaymentType = "Cash",
        Status = "Processing",
        CustomerName = "Chien Vu",
        CustomerPhone = "986665248",
        CustomerEmail = "chien.vh@gmail.com",
        CustomerAddress = "P806, Nha B2, Khu Do Thi My Dinh 1, My Dinh, Tu Liem, Ha Noi"
    }
);

        modelBuilder.Entity<Product>().HasData(
    new Product { ProductId = 1, ProductName = "Iphone 7 Rose 32GB", Image = "iPhone 6S 16GB Gold.jpg", Price = 849, UserId = 1, CategoryId = 1, ColorId = 1, ModelId = 1, StorageId = 1, SellStartDate = DateTime.Parse("2015-09-09").ToUniversalTime(), SellEndDate = DateTime.Parse("2016-09-09").ToUniversalTime(), IsNew = 1 },
    new Product { ProductId = 2, ProductName = "Iphone 6 Gold 16GB", Image = "iPhone 6S 16GB Gold.jpg", Price = 549, UserId = 1, CategoryId = 1, ColorId = 2, ModelId = 1, StorageId = 1, SellStartDate = DateTime.Parse("2014-09-09").ToUniversalTime(), SellEndDate = DateTime.Parse("2015-09-09").ToUniversalTime(), IsNew = 0 },
    new Product { ProductId = 3, ProductName = "Iphone 6S Rose 16GB", Image = "iPhone 6S 16GB Gold.jpg", Price = 649, UserId = 1, CategoryId = 1, ColorId = 3, ModelId = 2, StorageId = 1, SellStartDate = DateTime.Parse("2015-09-09").ToUniversalTime(), SellEndDate = DateTime.Parse("2015-09-09").ToUniversalTime(), IsNew = 1 },
    new Product { ProductId = 4, ProductName = "Iphone 6S Rose 16GB", Image = "iPhone 6S 16GB Gold.jpg", Price = 649, UserId = 1, CategoryId = 1, ColorId = 3, ModelId = 2, StorageId = 1, SellStartDate = DateTime.Parse("2015-09-09").ToUniversalTime(), SellEndDate = DateTime.Parse("2015-09-09").ToUniversalTime(), IsNew = 1 },
    new Product { ProductId = 5, ProductName = "Iphone 6S Rose 16GB", Image = "iPhone 6 128GB.jpg", Price = 649, UserId = 1, CategoryId = 1, ColorId = 3, ModelId = 2, StorageId = 1, SellStartDate = DateTime.Parse("2015-09-09").ToUniversalTime(), SellEndDate = DateTime.Parse("2015-09-09").ToUniversalTime(), IsNew = 1 },
    new Product { ProductId = 6, ProductName = "Iphone 6S Rose 16GB", Image = "iPhone 6S 16GB Gold.jpg", Price = 649, UserId = 1, CategoryId = 1, ColorId = 3, ModelId = 2, StorageId = 1, SellStartDate = DateTime.Parse("2015-09-09").ToUniversalTime(), SellEndDate = DateTime.Parse("2015-09-09").ToUniversalTime(), IsNew = 1 },
    new Product { ProductId = 7, ProductName = "Iphone 6S Rose 16GB", Image = "iPhone 6 128GB.jpg", Price = 649, UserId = 1, CategoryId = 1, ColorId = 3, ModelId = 2, StorageId = 1, SellStartDate = DateTime.Parse("2015-09-09").ToUniversalTime(), SellEndDate = DateTime.Parse("2015-09-09").ToUniversalTime(), IsNew = 1 },
    new Product { ProductId = 8, ProductName = "Iphone 6S Rose 16GB", Image = "iPhone 6 128GB.jpg", Price = 549, UserId = 1, CategoryId = 1, ColorId = 3, ModelId = 2, StorageId = 1, SellStartDate = DateTime.Parse("2015-09-09").ToUniversalTime(), SellEndDate = DateTime.Parse("2015-09-09").ToUniversalTime(), IsNew = 1 },
    new Product { ProductId = 9, ProductName = "Iphone 6 Gold 16GB", Image = "iPhone 6S 16GB Gold.jpg", Price = 649, UserId = 1, CategoryId = 1, ColorId = 3, ModelId = 2, StorageId = 1, SellStartDate = DateTime.Parse("2015-09-09").ToUniversalTime(), SellEndDate = DateTime.Parse("2015-09-09").ToUniversalTime(), IsNew = 1 },
    new Product { ProductId = 10, ProductName = "Iphone 6 Gold 16GB", Image = "iPhone 6 128GB.jpg", Price = 549, UserId = 1, CategoryId = 1, ColorId = 1, ModelId = 1, StorageId = 1, SellStartDate = DateTime.Parse("2015-09-09").ToUniversalTime(), SellEndDate = DateTime.Parse("2015-09-09").ToUniversalTime(), IsNew = 1 },
    new Product { ProductId = 11, ProductName = "Ipad Air 4", Image = "Ipad1.jpg", Price = 649, UserId = 1, CategoryId = 2, ColorId = 1, ModelId = 1, StorageId = 1, SellStartDate = DateTime.Parse("2015-09-09").ToUniversalTime(), SellEndDate = DateTime.Parse("2015-09-09").ToUniversalTime(), IsNew = 1 },
    new Product { ProductId = 12, ProductName = "Iphone 6 Gold 16GB", Image = "iPhone 6 128GB.jpg", Price = 649, UserId = 1, CategoryId = 2, ColorId = 1, ModelId = 1, StorageId = 1, SellStartDate = DateTime.Parse("2015-09-09").ToUniversalTime(), SellEndDate = DateTime.Parse("2015-09-09").ToUniversalTime(), IsNew = 1 },
    new Product { ProductId = 13, ProductName = "Iphone 6S Rose 16GB", Image = "iPhone 6 128GB.jpg", Price = 649, UserId = 1, CategoryId = 2, ColorId = 1, ModelId = 1, StorageId = 1, SellStartDate = DateTime.Parse("2015-09-09").ToUniversalTime(), SellEndDate = DateTime.Parse("2015-09-09").ToUniversalTime(), IsNew = 1 },
    new Product { ProductId = 14, ProductName = "Iphone 6 Gold 16GB", Image = "iPhone 6S 16GB Gold.jpg", Price = 649, UserId = 1, CategoryId = 2, ColorId = 1, ModelId = 1, StorageId = 2, SellStartDate = DateTime.Parse("2015-09-09").ToUniversalTime(), SellEndDate = DateTime.Parse("2015-09-09").ToUniversalTime(), IsNew = 1 },
    new Product { ProductId = 15, ProductName = "Iphone 6S Rose 16GB", Image = "iPhone 6 128GB.jpg", Price = 549, UserId = 1, CategoryId = 2, ColorId = 1, ModelId = 1, StorageId = 2, SellStartDate = DateTime.Parse("2015-09-09").ToUniversalTime(), SellEndDate = DateTime.Parse("2015-09-09").ToUniversalTime(), IsNew = 1 },
    new Product { ProductId = 16, ProductName = "Iphone 6 Gold 16GB", Image = "iPhone 6S 16GB Gold.jpg", Price = 549, UserId = 1, CategoryId = 2, ColorId = 1, ModelId = 1, StorageId = 2, SellStartDate = DateTime.Parse("2015-09-09").ToUniversalTime(), SellEndDate = DateTime.Parse("2015-09-09").ToUniversalTime(), IsNew = 1 },
    new Product { ProductId = 17, ProductName = "Macbook Pro 2016", Image = "macbook1.jpg", Price = 20, UserId = 1, CategoryId = 3, ColorId = 2, ModelId = 1, StorageId = 2, SellStartDate = DateTime.Parse("2015-09-09").ToUniversalTime(), SellEndDate = DateTime.Parse("2015-09-09").ToUniversalTime(), IsNew = 1 },
    new Product { ProductId = 18, ProductName = "Iphone 6S Rose 16GB", Image = "iPhone 6 128GB.jpg", Price = 649, UserId = 1, CategoryId = 3, ColorId = 2, ModelId = 1, StorageId = 2, SellStartDate = DateTime.Parse("2015-09-09").ToUniversalTime(), SellEndDate = DateTime.Parse("2015-09-09").ToUniversalTime(), IsNew = 1 },
    new Product { ProductId = 19, ProductName = "Iphone 6 Gold 16GB", Image = "macbook2.jpg", Price = 549, UserId = 1, CategoryId = 3, ColorId = 2, ModelId = 2, StorageId = 1, SellStartDate = DateTime.Parse("2015-09-09").ToUniversalTime(), SellEndDate = DateTime.Parse("2015-09-09").ToUniversalTime(), IsNew = 0 },
    new Product { ProductId = 20, ProductName = "Iphone 6 Gold 16GB", Image = "iPhone 6S 16GB Gold.jpg", Price = 549, UserId = 1, CategoryId = 3, ColorId = 2, ModelId = 2, StorageId = 1, SellStartDate = DateTime.Parse("2015-09-09").ToUniversalTime(), SellEndDate = DateTime.Parse("2015-09-09").ToUniversalTime(), IsNew = 0 },
    new Product { ProductId = 21, ProductName = "Iphone 6S Rose 16GB", Image = "iPhone 6 128GB.jpg", Price = 649, UserId = 1, CategoryId = 3, ColorId = 1, ModelId = 2, StorageId = 1, SellStartDate = DateTime.Parse("2015-09-09").ToUniversalTime(), SellEndDate = DateTime.Parse("2015-09-09").ToUniversalTime(), IsNew = 0 },
    new Product { ProductId = 22, ProductName = "Iphone 6 Gold 16GB", Image = "iPhone 6S 16GB Gold.jpg", Price = 549, UserId = 1, CategoryId = 1, ColorId = 1, ModelId = 2, StorageId = 1, SellStartDate = DateTime.Parse("2015-09-09").ToUniversalTime(), SellEndDate = DateTime.Parse("2015-09-09").ToUniversalTime(), IsNew = 1 },
    new Product { ProductId = 23, ProductName = "Iphone 6S Rose 16GB", Image = "iPhone 6 128GB.jpg", Price = 549, UserId = 1, CategoryId = 1, ColorId = 1, ModelId = 2, StorageId = 2, SellStartDate = DateTime.Parse("2015-09-09").ToUniversalTime(), SellEndDate = DateTime.Parse("2015-09-09").ToUniversalTime(), IsNew = 1 }
);

        modelBuilder.Entity<OrderDetail>().HasData(
                new OrderDetail { OrderID = 1, ProductID = 23, Price = 549, Quantity = 5 },
                new OrderDetail { OrderID = 1, ProductID = 20, Price = 649, Quantity = 3 },
                new OrderDetail { OrderID = 1, ProductID = 22, Price = 549, Quantity = 2 },
                new OrderDetail { OrderID = 2, ProductID = 22, Price = 549, Quantity = 2 },
                new OrderDetail { OrderID = 3, ProductID = 20, Price = 649, Quantity = 2 },
                new OrderDetail { OrderID = 4, ProductID = 17, Price = 549, Quantity = 1 },
                new OrderDetail { OrderID = 5, ProductID = 21, Price = 649, Quantity = 5 },
                new OrderDetail { OrderID = 6, ProductID = 22, Price = 549, Quantity = 2 },
                new OrderDetail { OrderID = 6, ProductID = 20, Price = 649, Quantity = 1 },
                new OrderDetail { OrderID = 6, ProductID = 23, Price = 549, Quantity = 1 },
                new OrderDetail { OrderID = 7, ProductID = 19, Price = 549, Quantity = 1 },
                new OrderDetail { OrderID = 7, ProductID = 21, Price = 649, Quantity = 1 },
                new OrderDetail { OrderID = 8, ProductID = 17, Price = 549, Quantity = 1 },
                new OrderDetail { OrderID = 8, ProductID = 20, Price = 549, Quantity = 1 },
                new OrderDetail { OrderID = 8, ProductID = 22, Price = 549, Quantity = 1 },
                new OrderDetail { OrderID = 8, ProductID = 14, Price = 649, Quantity = 1 },
                new OrderDetail { OrderID = 9, ProductID = 23, Price = 549, Quantity = 2 },
                new OrderDetail { OrderID = 9, ProductID = 20, Price = 649, Quantity = 1 },
                new OrderDetail { OrderID = 10, ProductID = 7, Price = 649, Quantity = 1 },
                new OrderDetail { OrderID = 10, ProductID = 15, Price = 549, Quantity = 1 }
            );

        modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, Username = "chienvh", Password = "chienvh" },
                new User { UserId = 2, Username = "admin", Password = "admin" }
            );
    }
}