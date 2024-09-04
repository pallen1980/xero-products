using Microsoft.AspNetCore.Mvc;
using XeroProducts.BL.Interfaces;
using XeroProducts.BL.Providers;
using XeroProducts.DAL;
using XeroProducts.DAL.Sql.Providers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DependencyInjection...
builder.Services.AddScoped<IProductOptionDALProvider, ProductOptionSqlProvider>();
builder.Services.AddScoped<IProductDALProvider, ProductSqlProvider>();
builder.Services.AddScoped<IProductOptionProvider, ProductOptionProvider>();
builder.Services.AddScoped<IProductProvider, ProductProvider>();

// Create our own validation filter to manually check modelstate and stop the automatic modelstate sending 400 responses for any validation failure
builder.Services.AddScoped<ValidationFilterAttribute>();
builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

// Create our own central point for handling exceptions
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails(); //for standardised error responses

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
