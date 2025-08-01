using Backend.Contexts;
using Backend.Interfaces;
using Backend.Models;
using Backend.Repositories;
using Backend.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

#region DbContext
builder.Services.AddDbContext<AppDbContext>(opts =>
{
    var connectionString = builder.Configuration.GetConnectionString("DatabaseConnectionString");
    opts.UseNpgsql(connectionString);
});
#endregion

#region CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});
#endregion

#region Repositories
builder.Services.AddTransient<IRepository<int, Category>, CategoryRepository>();
builder.Services.AddTransient<IRepository<int, Color>, ColorsRepository>();
builder.Services.AddTransient<IRepository<int, ContactUs>, ContactUsRepository>();
builder.Services.AddTransient<IRepository<int, News>, NewsRepository>();
builder.Services.AddTransient<IRepository<int, Order>, OrderRepository>();
builder.Services.AddTransient<IRepository<int, Product>, ProductRepository>();
builder.Services.AddTransient<IRepository<int, OrderDetail>, OrderDetailsRepository>();
builder.Services.AddTransient<IRepository<int, User>, UserRepository>();
#endregion

#region Services
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IColorService, ColorService>();
builder.Services.AddTransient<IContactUsService, ContactUsService>();
builder.Services.AddTransient<INewsService, NewsService>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IUserService, UserService>();
#endregion

var app = builder.Build();
app.UseCors();

QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
