using OrderService.Entities;
using Microsoft.EntityFrameworkCore;
using OrderService.Services;
using OrderService.Contracts;
using OrderService.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using OrderService.Entities.Model.DTOs;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddDbContext<ApplicationContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("OrderServiceCon") ??
throw new InvalidOperationException("Connections string: OrderServiceCon was not found")));



// Add services to the container.

builder.Services.AddControllers().AddXmlDataContractSerializerFormatters();
//builder.Services.AddControllers().AddXmlDataContractSerializerFormatters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureUnitOfWork();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));


builder.Services.AddScoped<ILoggerManager, LoggerManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
