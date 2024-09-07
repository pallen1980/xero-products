using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using XeroProducts.BL.Security;
using XeroProducts.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Add OpenAPI Documentation...
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    // Swagger/OpenAPI https://aka.ms/aspnetcore/swashbuckle

    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Xero Products API", Version = "v1" });

    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid JWT token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    option.IncludeXmlComments($"{AppContext.BaseDirectory}XeroProducts.Api.XML");

    option.OperationFilter<AuthResponseOperationFilter>();
});

// Setup DependencyInjection...
builder.AddDependencyConfig();

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
        ValidAudience = builder.Configuration.GetValue<string>("XeroProducts:Auth:JwtConfig:ValidAudience"),
        
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration.GetValue<string>("XeroProducts:Auth:JwtConfig:ValidIssuer"),
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
