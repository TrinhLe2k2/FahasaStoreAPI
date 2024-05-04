using FahasaStoreAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<FahasaStoreDBContext>(option => option.UseSqlServer
    (builder.Configuration.GetConnectionString("myStore")));

builder.Services.AddCors(p => p.AddPolicy("MyCors", build =>
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddMvc(option => option.EnableEndpointRouting = false)
                    .AddNewtonsoftJson(otp => otp.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MyCors");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
