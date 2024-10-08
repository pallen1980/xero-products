
using Newtonsoft.Json.Bson;
using XeroProducts.BL.Identity;
using XeroProducts.BL.Interfaces;
using XeroProducts.BL.Providers;
using XeroProducts.BL.Security;
using XeroProducts.DAL.EntityFramework.Sql.Contexts;
using XeroProducts.DAL.EntityFramework.Sql.Providers;
using XeroProducts.DAL.Interfaces;
using XeroProducts.DAL.Sql.Providers;
using XeroProducts.PasswordService.Interfaces;
using XeroProducts.PasswordService.Providers;

namespace XeroProducts.Api.Extensions
{
    public static class SetupServiceExtension
    {
        public static void AddDependencyConfig(this WebApplicationBuilder builder)
        {
            //Allow dependencies to use builder.Configuration without needing to pass it as an argument to methods
            builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
            builder.Services.AddSingleton(provider => new Lazy<IConfiguration>(() => provider.GetRequiredService<IConfiguration>()));

            // - DAL
            //   Check config and use the preferred DAL type
            switch (builder.Configuration.GetValue<string>("XeroProducts:DAL:Type"))
            {
                //Setup SQL-Commend-type DAL...
                case "SQL":
                    AddDependencyConfigFor_SQLCommandDAL(builder);
                    break;

                //Default is EntityFramework-type DAL
                default:
                    AddDependencyConfigFor_EntityFramework(builder);
                    break;
            }

            // - BL
            builder.Services.AddScoped<IProductOptionProvider, ProductOptionProvider>();
            builder.Services.AddScoped<IProductProvider, ProductProvider>();
            builder.Services.AddScoped<IUserProvider, UserProvider>();
            builder.Services.AddScoped<IJwtTokenProvider, JwtTokenProvider>();
            // - BL Lazy loading Support
            builder.Services.AddScoped(provider => new Lazy<IProductOptionProvider>(() => provider.GetRequiredService<IProductOptionProvider>()));
            builder.Services.AddScoped(provider => new Lazy<IProductProvider>(() => provider.GetRequiredService<IProductProvider>()));
            builder.Services.AddScoped(provider => new Lazy<IUserProvider>(() => provider.GetRequiredService<IUserProvider>()));
            builder.Services.AddScoped(provider => new Lazy<IJwtTokenProvider>(() => provider.GetRequiredService<IJwtTokenProvider>()));

            // - Tools
            builder.Services.AddScoped<IPasswordProvider, PasswordProvider>();
            builder.Services.AddScoped(provider => new Lazy<IPasswordProvider>(() => provider.GetRequiredService<IPasswordProvider>()));
        }

        /// <summary>
        /// Setup all DI for Direct SQL Command-type DAL
        /// </summary>
        /// <param name="builder"></param>
        private static void AddDependencyConfigFor_SQLCommandDAL(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IProductOptionDALProvider, ProductOptionSqlProvider>();
            builder.Services.AddScoped<IProductDALProvider, ProductSqlProvider>();
            builder.Services.AddScoped<IUserDALProvider, UserSqlProvider>();

            //For lazy-loading support
            builder.Services.AddScoped(provider => new Lazy<IProductOptionDALProvider>(() => provider.GetRequiredService<IProductOptionDALProvider>()));
            builder.Services.AddScoped(provider => new Lazy<IProductDALProvider>(() => provider.GetRequiredService<IProductDALProvider>()));
            builder.Services.AddScoped(provider => new Lazy<IUserDALProvider>(() => provider.GetRequiredService<IUserDALProvider>()));
        }

        /// <summary>
        /// Setup all DI for EntityFramework DAL
        /// </summary>
        /// <param name="builder"></param>
        private static void AddDependencyConfigFor_EntityFramework(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IXeroProductsContext, XeroProductsContext>();
            builder.Services.AddScoped<IProductOptionDALProvider, ProductOptionEntityFrameworkSqlProvider>();
            builder.Services.AddScoped<IProductDALProvider, ProductEntityFrameworkSqlProvider>();
            builder.Services.AddScoped<IUserDALProvider, UserEntityFrameworkSqlProvider>();

            //For lazy-loading support
            builder.Services.AddScoped(provider => new Lazy<IXeroProductsContext>(() => provider.GetRequiredService<IXeroProductsContext>()));
            builder.Services.AddScoped(provider => new Lazy<IProductOptionDALProvider>(() => provider.GetRequiredService<IProductOptionDALProvider>()));
            builder.Services.AddScoped(provider => new Lazy<IProductDALProvider>(() => provider.GetRequiredService<IProductDALProvider>()));
            builder.Services.AddScoped(provider => new Lazy<IUserDALProvider>(() => provider.GetRequiredService<IUserDALProvider>()));
        }
    }
}