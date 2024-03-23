using OrderService.Entities;
using Microsoft.EntityFrameworkCore;
using OrderService.Services;
using OrderService.Contracts;
using OrderService.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using OrderService.Exceptions;

var builder = WebApplication.CreateBuilder(args);


//builder.Services.AddDbContext<ApplicationContext>(
//    options =>
//options.UseSqlServer(builder.Configuration.GetConnectionString("OrderServiceConPQL") ??
//throw new InvalidOperationException("Connections string: OrderServiceCon was not found")));


builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("OrderServiceConPQL") ??
throw new InvalidOperationException("Connections string: OrderServiceCon was not found")));

/// ==========================
/// Register AutoMapper
/// ==========================
// builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.ConfigureAutoMapper();


// Add services to the container.

builder.Services.AddControllers().AddXmlDataContractSerializerFormatters();
//builder.Services.AddControllers().AddXmlDataContractSerializerFormatters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureUnitOfWork();

builder.Services.AddScoped<ILoggerManager, LoggerManager>();

// ========================
// Anwar
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//builder.Services.AddExceptionHandler<AppExceptionHandler>();
// ========================


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ========================
// Anwar
//app.UseExceptionHandler();
// ========================

app.UseHttpsRedirection();
//app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
