using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using XeroProducts.BL.Authentication;
using XeroProducts.BL.Interfaces;
using XeroProducts.BL.Providers;
using XeroProducts.DAL.Interfaces;
using XeroProducts.DAL.Sql.Providers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Add OpenAPI Documentation...
builder.Services.AddSwaggerGen(option =>
{
    //option.SwaggerDoc("v1.0", new OpenApiInfo { Title = "Xero Products API", Version = "v1.0" });

    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter the JWT token here...",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// DependencyInjection...
builder.Services.AddScoped<IProductOptionDALProvider, ProductOptionSqlProvider>();
builder.Services.AddScoped<IProductDALProvider, ProductSqlProvider>();
builder.Services.AddScoped<IProductOptionProvider, ProductOptionProvider>();
builder.Services.AddScoped<IProductProvider, ProductProvider>();
builder.Services.AddScoped<IJwtTokenProvider, JwtTokenProvider>();

// Create our own validation filter to manually check modelstate and stop the automatic modelstate sending 400 responses for any validation failure
builder.Services.AddScoped<ValidationFilterAttribute>();
builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

// Create our own central point for handling exceptions
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails(); //for standardised error responses

// Add Jwt Token-Based Authentication
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(jwtOption =>
{
    //grab the unique key for this site...
    var keyBytes = JwtTokenProvider.GetJwtKeyBytes(builder.Configuration);

    jwtOption.SaveToken = true;
    jwtOption.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        
        ValidateLifetime = true,
        
        ValidateAudience = true,
        ValidAudience = builder.Configuration.GetValue<string>("Auth:JwtConfig:ValidAudience"),
        
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration.GetValue<string>("Auth:JwtConfig:ValidIssuer"),
    };
});

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
