using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using FahasaStoreAPI.Identity;
using FahasaStoreAPI.Services;
using FahasaStoreAPI.Entities;
using FahasaStoreAPI;
using FahasaStoreAPI.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Fahasa Store API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
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

builder.Services.AddDbContext<StoreContext>(option => option.UseSqlServer
    (builder.Configuration.GetConnectionString("myStore")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<StoreContext>().AddDefaultTokenProviders();

builder.Services.AddDbContext<FahasaStoreDBContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("myStore"));
    //option.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddScoped<IImageUploader, ImageUploader>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IBookRecommendationSystem, BookRecommendationSystem>();

builder.Services.AddCors(p => p.AddPolicy("MyCors", build =>
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

// Life cycle DI: AddSingleton(), AddTransient(), AddScoped()
builder.Services.AddMvc(option => option.EnableEndpointRouting = false)
                    .AddNewtonsoftJson(otp => otp.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddHttpClient();

// Add Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters 
    {
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"])),
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(AppRole.Admin, policy =>
    {
        policy.RequireRole(AppRole.Admin);
    });
    options.AddPolicy(AppRole.Staff, policy =>
    {
        policy.RequireRole(AppRole.Staff);
    });
    options.AddPolicy(AppRole.Customer, policy =>
    {
        policy.RequireRole(AppRole.Customer);
    });
    options.AddPolicy("AdminOrStaff", policy =>
    {
        policy.RequireRole(AppRole.Admin, AppRole.Staff);
    });
});


builder.Services.Configure<IdentityOptions>(options =>
{
    // Cấu hình Password
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = false;

    // Cấu hình Lockout
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // Cấu hình User
    options.User.RequireUniqueEmail = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MyCors");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
