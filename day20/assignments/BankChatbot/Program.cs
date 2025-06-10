using BankChatbot.AI;
using BankChatbot.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IChatHistoryStore, ChatHistoryStore>();
builder.Services.AddScoped<IChatbot, Chatbot>();
builder.Services.AddScoped<IChatbotService, ChatbotService>();

var cfg = builder.Configuration.GetSection("Secrets");
builder.Services.Configure<Secrets>(cfg);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();