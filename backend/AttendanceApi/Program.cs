using System.Security.Claims;
using System.Text;
using System.Threading.RateLimiting;
using AttendanceApi.Contexts;
using AttendanceApi.Interfaces;
using AttendanceApi.Misc;
using AttendanceApi.Misc.Authentications.Handlers;
using AttendanceApi.Misc.Authentications.Requirements;
using AttendanceApi.Models;
using AttendanceApi.Models.DTOs;
using AttendanceApi.Repositories;
using AttendanceApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Logging.AddConsole();

builder.Services.AddControllers(opts =>
                {
                    opts.Filters.Add<LogRequestFilter>();
                    opts.Filters.Add<ApiResponseFilterAttribute>();

                })
                .AddJsonOptions(opts =>
                {
                    opts.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                    opts.JsonSerializerOptions.WriteIndented = true;
                });
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Attendance API", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

#region DbContext
builder.Services.AddDbContext<AttendanceContext>(opts =>
{
    opts.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
#endregion

#region Repositories
builder.Services.AddTransient<IRepository<int, Session>, SessionRepository>();
builder.Services.AddTransient<IRepository<int, SessionAttendance>, SessionAttendanceRepository>();
builder.Services.AddTransient<IRepository<int, Student>, StudentRepository>();
builder.Services.AddTransient<IRepository<int, Teacher>, TeacherRepository>();
builder.Services.AddTransient<IRepository<string, User>, UserRepository>();
#endregion

#region Services
builder.Services.AddTransient<ISessionService, SessionService>();
builder.Services.AddTransient<ITeacherService, TeacherService>();
builder.Services.AddTransient<IStudentService, StudentService>();
builder.Services.AddTransient<IEncryptionService, EncryptionService>();
builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<IOwnerService, OwnerService>();
builder.Services.AddTransient<IAttendanceService, AttendanceService>();
#endregion

#region AutoMapper
builder.Services.AddAutoMapper(typeof(User));
builder.Services.AddAutoMapper(typeof(Teacher));
builder.Services.AddAutoMapper(typeof(Student));
#endregion

#region CORS
builder.Services.AddCors(options=>{
    options.AddDefaultPolicy(policy=>{
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});
#endregion

#region Misc
builder.Logging.AddLog4Net();
builder.Services.AddSignalR();
builder.Services.AddHttpContextAccessor();
builder.Services.AddApiVersioning(opts =>
{
    opts.AssumeDefaultVersionWhenUnspecified = true;
    opts.DefaultApiVersion = new ApiVersion(1, 0);
    opts.ReportApiVersions = true;
    opts.ApiVersionReader = new UrlSegmentApiVersionReader();
});
builder.Services.AddRateLimiter(opts =>
{
    opts.RejectionStatusCode = 429;
    opts.OnRejected = async (context, token) =>
    {
        var response = new ApiResponseDTO { Data = null, ErrorMessage = "Rate limit exceeded", Success = false };
        var json = System.Text.Json.JsonSerializer.Serialize(response);
        context.HttpContext.Response.ContentType = "application/json";
        await context.HttpContext.Response.WriteAsync(json, token);
    };
    opts.AddPolicy("PerUserOrIpPolicy", context =>
    {
        var username = context.User.Identity?.Name;

        if (username == null)
            username = context.Connection.RemoteIpAddress.ToString();
        
        return RateLimitPartition.GetTokenBucketLimiter(username, key => new TokenBucketRateLimiterOptions
        {
            TokenLimit = 1000,
            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
            QueueLimit = 0,
            ReplenishmentPeriod = TimeSpan.FromHours(1),
            TokensPerPeriod = 1000,
            AutoReplenishment = true
        });
        
    });
});;
#endregion

#region AuthenticationFilter
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Keys:JwtTokenKey"]))
                    };
                });
#endregion

#region AuthorizationHandlers
builder.Services.AddScoped<IAuthorizationHandler, IsOwnerHandler>();
#endregion

#region Policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("IsOwner", policy =>
        policy.Requirements.Add(new IsOwnerRequirement()));
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors();
app.MapHub<NotificationHub>("/notification");

app.UseRateLimiter();
app.MapControllers().RequireRateLimiting("PerUserOrIpPolicy");

app.Run();
