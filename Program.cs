
using Ecommerce.Api.Middleware;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.validators.Product;
using Ecommerce.Infrastructure.Data;
using Ecommerce.Infrastructure.Repositories;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//db ni register chesa
builder.Services.AddDbContext<EcommerceDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IProductRepository,ProductRepository>();

//fluentvalidation ni register chesthunna 

builder.Services.AddControllers()
    .AddFluentValidation(fv =>
    fv.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>());



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
