using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    ColorId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.ColorId);
                });

            migrationBuilder.CreateTable(
                name: "ContactUs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Models",
                columns: table => new
                {
                    ModelId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Models", x => x.ModelId);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderName = table.Column<string>(type: "text", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PaymentType = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CustomerName = table.Column<string>(type: "text", nullable: false),
                    CustomerPhone = table.Column<string>(type: "text", nullable: false),
                    CustomerEmail = table.Column<string>(type: "text", nullable: false),
                    CustomerAddress = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    NewsId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: false),
                    ShortDescription = table.Column<string>(type: "text", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.NewsId);
                    table.ForeignKey(
                        name: "FK_News_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductName = table.Column<string>(type: "text", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    CategoryId = table.Column<int>(type: "integer", nullable: true),
                    ColorId = table.Column<int>(type: "integer", nullable: true),
                    ModelId = table.Column<int>(type: "integer", nullable: true),
                    StorageId = table.Column<int>(type: "integer", nullable: true),
                    SellStartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SellEndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsNew = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId");
                    table.ForeignKey(
                        name: "FK_Products_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "ColorId");
                    table.ForeignKey(
                        name: "FK_Products_Models_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Models",
                        principalColumn: "ModelId");
                    table.ForeignKey(
                        name: "FK_Products_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "integer", nullable: false),
                    ProductID = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: true),
                    Quantity = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => new { x.OrderID, x.ProductID });
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name" },
                values: new object[,]
                {
                    { 1, "Iphone" },
                    { 2, "Ipad" },
                    { 3, "Macbook" }
                });

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "ColorId", "Name" },
                values: new object[,]
                {
                    { 1, "Rose" },
                    { 2, "Gold" },
                    { 3, "White" },
                    { 4, "Black" },
                    { 5, "Grey" }
                });

            migrationBuilder.InsertData(
                table: "ContactUs",
                columns: new[] { "Id", "Content", "Email", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, "What the hell?", "chien.vh@gmail.com", "Chien Vu", "986665248" },
                    { 2, "2nd time for submitting form. thanks", "chien.vh@gmail.com", "Chien Vu Hoang", "986665248" }
                });

            migrationBuilder.InsertData(
                table: "Models",
                columns: new[] { "ModelId", "Name" },
                values: new object[,]
                {
                    { 1, "6S Rose Gold" },
                    { 2, "6S Gold" },
                    { 3, "6 Gold" },
                    { 4, "6 Grey" },
                    { 5, "6S Rose Plus" },
                    { 6, "6S Gold Plus" },
                    { 7, "6 Gold Plus" },
                    { 8, "6 Grey Plus" },
                    { 9, "5S Gold" },
                    { 10, "5S Black" },
                    { 11, "5S White" },
                    { 12, "5 White" },
                    { 13, "5 Black" },
                    { 14, "Ipad Mini 1" },
                    { 15, "Ipad Mini 2" },
                    { 16, "Ipad Mini 3" },
                    { 17, "Ipad Mini 4" },
                    { 18, "Ipad 2" },
                    { 19, "Ipad 3" },
                    { 20, "Ipad 4" },
                    { 21, "Ipad Air" },
                    { 22, "Macbook Pro" },
                    { 23, "Macbook Pro Retina" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderID", "CustomerAddress", "CustomerEmail", "CustomerName", "CustomerPhone", "OrderDate", "OrderName", "PaymentType", "Status" },
                values: new object[,]
                {
                    { 1, "My Dinh, Ha Noi, Viet Nam", "chien.vh@gmail.com", "Chien Vu", "84986665248", new DateTime(2017, 7, 1, 18, 30, 0, 0, DateTimeKind.Utc), "Order 1", "Cash", "Paid" },
                    { 2, "Ha Noi, Viet Nam", "chien.vh@hotmail.com", "Chien Vu Hoang", "84986665248", new DateTime(2017, 7, 6, 18, 30, 0, 0, DateTimeKind.Utc), "Order 2", "Cash", "Processing" },
                    { 3, "My Dinh, Ha Noi, Viet Nam", "chien.vh@gmail.com", "Hoang Hop", "986665248", new DateTime(2017, 8, 25, 18, 30, 0, 0, DateTimeKind.Utc), "Order 3", "Cash", "Processing" },
                    { 4, "My Dinh, Ha Noi, Viet Nam", "chien.vh@gmail.com", "Hop Hoang", "986665248", new DateTime(2017, 8, 25, 18, 30, 0, 0, DateTimeKind.Utc), "Order 4", "Cash", "Processing" },
                    { 5, "My Dinh, Ha Noi, Viet Nam", "chien.vh@gmail.com", "Chien VH", "986665248", new DateTime(2017, 8, 25, 18, 30, 0, 0, DateTimeKind.Utc), "Order 5", "Cash", "Processing" },
                    { 6, "My Dinh, Ha Noi, Viet Nam", "chien.vh@gmail.com", "Chien", "986665248", new DateTime(2017, 8, 25, 18, 30, 0, 0, DateTimeKind.Utc), "Order 6", "Cash", "Processing" },
                    { 7, "My Dinh, Ha Noi, Viet Nam", "chien.vh@gmail.com", "Chien Hoang", "986665248", new DateTime(2017, 8, 25, 18, 30, 0, 0, DateTimeKind.Utc), "Order 7", "Cash", "Processing" },
                    { 8, "Ha Noi, Viet Nam", "chien.vh@gmail.com", "Hop HT", "986665248", new DateTime(2017, 8, 26, 18, 30, 0, 0, DateTimeKind.Utc), "Order 8", "Cash", "Processing" },
                    { 9, "Ha Noi, Viet Nam", "chien.vh@gmail.com", "Chien Vu", "986665248", new DateTime(2017, 8, 26, 18, 30, 0, 0, DateTimeKind.Utc), "Order 9", "Cash", "Processing" },
                    { 10, "P806, Nha B2, Khu Do Thi My Dinh 1, My Dinh, Tu Liem, Ha Noi", "chien.vh@gmail.com", "Chien Vu", "986665248", new DateTime(2017, 9, 2, 18, 30, 0, 0, DateTimeKind.Utc), "Order 10", "Cash", "Processing" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Password", "Username" },
                values: new object[,]
                {
                    { 1, "chienvh", "chienvh" },
                    { 2, "admin", "admin" }
                });

            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "NewsId", "Content", "CreatedDate", "Image", "ShortDescription", "Status", "Title", "UserId" },
                values: new object[,]
                {
                    { 2, "Content should be here. Will be updated later", new DateTime(2015, 11, 10, 18, 30, 0, 0, DateTimeKind.Utc), "iPhone 6 Plus 16Gb Grey.jpg", "This is the short description first", 1, "My Testing First", 1 },
                    { 3, "Content should be here. Will be updated later", new DateTime(2015, 11, 10, 18, 30, 0, 0, DateTimeKind.Utc), "iPhone 5S 32GB Silver.jpg", "This is the short description 2", 1, "My Testing 2", 1 },
                    { 4, "Content should be here. Will be updated later", new DateTime(2015, 11, 10, 18, 30, 0, 0, DateTimeKind.Utc), "iPhone 6 Plus 16Gb Grey.jpg", "This is the short description 3", 1, "My Testing 3", 1 },
                    { 5, "Content should be here. Will be updated later", new DateTime(2015, 11, 10, 18, 30, 0, 0, DateTimeKind.Utc), "iPhone 5S 32GB Silver.jpg", "This is the short description 4", 1, "My Testing 4", 1 },
                    { 6, "Content should be here. Will be updated later", new DateTime(2015, 11, 10, 18, 30, 0, 0, DateTimeKind.Utc), "iPhone 6 Plus 16Gb Grey.jpg", "This is the short description 5", 1, "My Testing 5", 1 },
                    { 7, "Content should be here. Will be updated later", new DateTime(2015, 11, 10, 18, 30, 0, 0, DateTimeKind.Utc), "iPhone 5S 32GB Silver.jpg", "This is the short description 6", 1, "My Testing 6", 1 },
                    { 8, "Content should be here. Will be updated later", new DateTime(2015, 11, 10, 18, 30, 0, 0, DateTimeKind.Utc), "iPhone 5S 32GB Silver.jpg", "This is the short description 7", 1, "My Testing 7", 1 },
                    { 9, "Content should be here. Will be updated later", new DateTime(2015, 11, 10, 18, 30, 0, 0, DateTimeKind.Utc), "iPhone 5S 32GB Silver.jpg", "This is the short description 8", 1, "My Testing 8", 1 },
                    { 10, "Content should be here. Will be updated later", new DateTime(2015, 11, 10, 18, 30, 0, 0, DateTimeKind.Utc), "iPhone 6 Plus 16Gb Grey.jpg", "This is the short description 9", 1, "My Testing 9", 1 },
                    { 11, "Content should be here. Will be updated later", new DateTime(2015, 11, 10, 18, 30, 0, 0, DateTimeKind.Utc), "iPhone 5S 32GB Silver.jpg", "This is the short description 10", 1, "My Testing 10", 1 },
                    { 12, "Content should be here. Will be updated later", new DateTime(2015, 11, 10, 18, 30, 0, 0, DateTimeKind.Utc), "iPhone 6 Plus 16Gb Grey.jpg", "This is the short description 11", 1, "My Testing 11", 1 },
                    { 13, "Content should be here. Will be updated later", new DateTime(2015, 11, 10, 18, 30, 0, 0, DateTimeKind.Utc), "iPhone 5S 32GB Silver.jpg", "This is the short description 12", 1, "My Testing 12", 1 },
                    { 14, "Content should be here. Will be updated later", new DateTime(2015, 11, 10, 18, 30, 0, 0, DateTimeKind.Utc), "iPhone 5S 32GB Silver.jpg", "This is the short description 13", 1, "My Testing 13", 1 },
                    { 15, "Content should be here. Will be updated later", new DateTime(2015, 11, 10, 18, 30, 0, 0, DateTimeKind.Utc), "iPhone 6 Plus 16Gb Grey.jpg", "This is the short description 14", 1, "My Testing 14", 1 },
                    { 16, "Content should be here. Will be updated later", new DateTime(2015, 11, 10, 18, 30, 0, 0, DateTimeKind.Utc), "iPhone 6 Plus 16Gb Grey.jpg", "This is the short description 15", 1, "My Testing 15", 1 },
                    { 17, "Content should be here. Will be updated later", new DateTime(2015, 11, 10, 18, 30, 0, 0, DateTimeKind.Utc), "iPhone 6 Plus 16Gb Grey.jpg", "This is the short description 16", 1, "My Testing 16", 1 },
                    { 18, "Content should be here. Will be updated later", new DateTime(2015, 11, 10, 18, 30, 0, 0, DateTimeKind.Utc), "iPhone 5S 32GB Silver.jpg", "This is the short description 17", 1, "My Testing 17", 1 },
                    { 19, "Content should be here. Will be updated later", new DateTime(2015, 11, 10, 18, 30, 0, 0, DateTimeKind.Utc), "iPhone 5S 32GB Silver.jpg", "This is the short description 18", 1, "My Testing 18", 1 },
                    { 20, "Content should be here. Will be updated later", new DateTime(2015, 11, 10, 18, 30, 0, 0, DateTimeKind.Utc), "iPhone 6 Plus 16Gb Grey.jpg", "This is the short description 19", 1, "My Testing 19", 1 },
                    { 21, "Content should be here. Will be updated later", new DateTime(2015, 11, 10, 18, 30, 0, 0, DateTimeKind.Utc), "iPhone 6 Plus 16Gb Grey.jpg", "This is the short description 20", 1, "My Testing 20", 1 },
                    { 22, "Content should be here. Will be updated later", new DateTime(2015, 11, 10, 18, 30, 0, 0, DateTimeKind.Utc), "iPhone 6 Plus 16Gb Grey.jpg", "This is the short description 21", 1, "My Testing 21", 1 },
                    { 23, "Content should be here. Will be updated later", new DateTime(2015, 11, 10, 18, 30, 0, 0, DateTimeKind.Utc), "iPhone 5S 32GB Silver.jpg", "This is the short description 22", 1, "My Testing 22", 1 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "ColorId", "Image", "IsNew", "ModelId", "Price", "ProductName", "SellEndDate", "SellStartDate", "StorageId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 1, "iPhone 6S 16GB Gold.jpg", 1, 1, 849.0, "Iphone 7 Rose 32GB", new DateTime(2016, 9, 8, 18, 30, 0, 0, DateTimeKind.Utc), new DateTime(2015, 9, 8, 18, 30, 0, 0, DateTimeKind.Utc), 1, 1 },
                    { 2, 1, 2, "iPhone 6S 16GB Gold.jpg", 0, 1, 549.0, "Iphone 6 Gold 16GB", new DateTime(2015, 9, 8, 18, 30, 0, 0, DateTimeKind.Utc), new DateTime(2014, 9, 8, 18, 30, 0, 0, DateTimeKind.Utc), 1, 1 },
                    { 3, 1, 3, "iPhone 6S 16GB Gold.jpg", 1, 2, 649.0, "Iphone 6S Rose 16GB", new DateTime(2015, 9, 8, 18, 30, 0, 0, DateTimeKind.Utc), new DateTime(2015, 9, 8, 18, 30, 0, 0, DateTimeKind.Utc), 1, 1 },
                    { 4, 1, 3, "iPhone 6S 16GB Gold.jpg", 1, 2, 649.0, "Iphone 6S Rose 16GB", new DateTime(2015, 9, 8, 18, 30, 0, 0, DateTimeKind.Utc), new DateTime(2015, 9, 8, 18, 30, 0, 0, DateTimeKind.Utc), 1, 1 },
                    { 5, 1, 3, "iPhone 6 128GB.jpg", 1, 2, 649.0, "Iphone 6S Rose 16GB", new DateTime(2015, 9, 8, 18, 30, 0, 0, DateTimeKind.Utc), new DateTime(2015, 9, 8, 18, 30, 0, 0, DateTimeKind.Utc), 1, 1 },
                    { 6, 1, 3, "iPhone 6S 16GB Gold.jpg", 1, 2, 649.0, "Iphone 6S Rose 16GB", new DateTime(2015, 9, 8, 18, 30, 0, 0, DateTimeKind.Utc), new DateTime(2015, 9, 8, 18, 30, 0, 0, DateTimeKind.Utc), 1, 1 },
                    { 7, 1, 3, "iPhone 6 128GB.jpg", 1, 2, 649.0, "Iphone 6S Rose 16GB", new DateTime(2015, 9, 8, 18, 30, 0, 0, DateTimeKind.Utc), new DateTime(2015, 9, 8, 18, 30, 0, 0, DateTimeKind.Utc), 1, 1 },
                    { 8, 1, 3, "iPhone 6 128GB.jpg", 1, 2, 549.0, "Iphone 6S Rose 16GB", new DateTime(2015, 9, 8, 18, 30, 0, 0, DateTimeKind.Utc), new DateTime(2015, 9, 8, 18, 30, 0, 0, DateTimeKind.Utc), 1, 1 },
                    { 9, 1, 3, "iPhone 6S 16GB Gold.jpg", 1, 2, 649.0, "Iphone 6 Gold 16GB", new DateTime(2015, 9, 8, 18, 30, 0, 0, DateTimeKind.Utc), new DateTime(2015, 9, 8, 18, 30, 0, 0, DateTimeKind.Utc), 1, 1 },
                    { 10, 1, 1, "iPhone 6 128GB.jpg", 1, 1, 549.0, "Iphone 6 Gold 16GB", new DateTime(2015, 9, 8, 18, 30, 0, 0, DateTimeKind.Utc), new DateTime(2015, 9, 8, 18, 30, 0, 0, DateTimeKind.Utc), 1, 1 },
                    { 11, 2, 1, "Ipad1.jpg", 1, 1, 649.0, "Ipad Air 4", new DateTime(2015, 9, 8, 18, 30, 0, 0, DateTimeKind.Utc), new DateTime(2015, 9, 8, 18, 30, 0, 0, DateTimeKind.Utc), 1, 1 },
                    { 12, 2, 1, "iPhone 6 128GB.jpg", 1, 1, 649.0, "Iphone 6 Gold 16GB", new DateTime(2015, 9, 8, 18, 30, 0, 0, DateTimeKind.Utc), new DateTime(2015, 9, 8, 18, 30, 0, 0, DateTimeKind.Utc), 1, 1 },
                    { 13, 2, 1, "iPhone 6 128GB.jpg", 1, 1, 649.0, "Iphone 6S Rose 16GB", new DateTime(2015, 9, 8, 18, 30, 0, 0, DateTimeKind.Utc), new DateTime(2015, 9, 8, 18, 30, 0, 0, DateTimeKind.Utc), 1, 1 },
                    { 14, 2, 1, "iPhone 6S 16GB Gold.jpg", 1, 1, 649.0, "Iphone 6 Gold 16GB", new DateTime(2015, 9, 8, 18, 30, 0, 0, DateTimeKind.Utc), new DateTime(2015, 9, 8, 18, 30, 0, 0, DateTimeKind.Utc), 2, 1 },
                    { 15, 2, 1, "iPhone 6 128GB.jpg", 1, 1, 549.0, "Iphone 6S Rose 16GB", new DateTime(2015, 9, 8, 18, 30, 0, 0, DateTimeKind.Utc), new DateTime(2015, 9, 8, 18, 30, 0, 0, DateTimeKind.Utc), 2, 1 },
                    { 16, 2, 1, "iPhone 6S 16GB Gold.jpg", 1, 1, 549.0, "Iphone 6 Gold 16GB", new DateTime(2015, 9, 8, 18, 30, 0, 0, DateTimeKind.Utc), new DateTime(2015, 9, 8, 18, 30, 0, 0, DateTimeKind.Utc), 2, 1 },
                    { 17, 3, 2, "macbook1.jpg", 1, 1, 20.0, "Macbook Pro 2016", new DateTime(2015, 9, 8, 18, 30, 0, 0, DateTimeKind.Utc), new DateTime(2015, 9, 8, 18, 30, 0, 0, DateTimeKind.Utc), 2, 1 },
                    { 18, 3, 2, "iPhone 6 128GB.jpg", 1, 1, 649.0, "Iphone 6S Rose 16GB", new DateTime(2015, 9, 8, 18, 30, 0, 0, DateTimeKind.Utc), new DateTime(2015, 9, 8, 18, 30, 0, 0, DateTimeKind.Utc), 2, 1 },
                    { 19, 3, 2, "macbook2.jpg", 0, 2, 549.0, "Iphone 6 Gold 16GB", new DateTime(2015, 9, 8, 18, 30, 0, 0, DateTimeKind.Utc), new DateTime(2015, 9, 8, 18, 30, 0, 0, DateTimeKind.Utc), 1, 1 },
                    { 20, 3, 2, "iPhone 6S 16GB Gold.jpg", 0, 2, 549.0, "Iphone 6 Gold 16GB", new DateTime(2015, 9, 8, 18, 30, 0, 0, DateTimeKind.Utc), new DateTime(2015, 9, 8, 18, 30, 0, 0, DateTimeKind.Utc), 1, 1 },
                    { 21, 3, 1, "iPhone 6 128GB.jpg", 0, 2, 649.0, "Iphone 6S Rose 16GB", new DateTime(2015, 9, 8, 18, 30, 0, 0, DateTimeKind.Utc), new DateTime(2015, 9, 8, 18, 30, 0, 0, DateTimeKind.Utc), 1, 1 },
                    { 22, 1, 1, "iPhone 6S 16GB Gold.jpg", 1, 2, 549.0, "Iphone 6 Gold 16GB", new DateTime(2015, 9, 8, 18, 30, 0, 0, DateTimeKind.Utc), new DateTime(2015, 9, 8, 18, 30, 0, 0, DateTimeKind.Utc), 1, 1 },
                    { 23, 1, 1, "iPhone 6 128GB.jpg", 1, 2, 549.0, "Iphone 6S Rose 16GB", new DateTime(2015, 9, 8, 18, 30, 0, 0, DateTimeKind.Utc), new DateTime(2015, 9, 8, 18, 30, 0, 0, DateTimeKind.Utc), 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "OrderDetails",
                columns: new[] { "OrderID", "ProductID", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, 20, 649.0, 3 },
                    { 1, 22, 549.0, 2 },
                    { 1, 23, 549.0, 5 },
                    { 2, 22, 549.0, 2 },
                    { 3, 20, 649.0, 2 },
                    { 4, 17, 549.0, 1 },
                    { 5, 21, 649.0, 5 },
                    { 6, 20, 649.0, 1 },
                    { 6, 22, 549.0, 2 },
                    { 6, 23, 549.0, 1 },
                    { 7, 19, 549.0, 1 },
                    { 7, 21, 649.0, 1 },
                    { 8, 14, 649.0, 1 },
                    { 8, 17, 549.0, 1 },
                    { 8, 20, 549.0, 1 },
                    { 8, 22, 549.0, 1 },
                    { 9, 20, 649.0, 1 },
                    { 9, 23, 549.0, 2 },
                    { 10, 7, 649.0, 1 },
                    { 10, 15, 549.0, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_News_UserId",
                table: "News",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductID",
                table: "OrderDetails",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ColorId",
                table: "Products",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ModelId",
                table: "Products",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UserId",
                table: "Products",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactUs");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropTable(
                name: "Models");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
