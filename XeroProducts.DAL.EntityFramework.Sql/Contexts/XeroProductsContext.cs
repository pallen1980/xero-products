using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using XeroProducts.DAL.Helpers;
using XeroProducts.Types;

namespace XeroProducts.DAL.EntityFramework.Sql.Contexts
{
    public partial class XeroProductsContext : DbContext, IXeroProductsContext
    {
        private readonly Lazy<IConfiguration> _configuration;
        protected IConfiguration Configuration => _configuration.Value;

        public XeroProductsContext(Lazy<IConfiguration> configuration)
        {
            _configuration = configuration;
        }

        public XeroProductsContext(DbContextOptions<XeroProductsContext> options,
                                   Lazy<IConfiguration> configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<ProductOption> ProductOptions { get; set; }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionHelper.GetDefaultConnectionString(Configuration));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK__Product__3214EC06B29B4126")
                    .IsClustered(false);

                entity.ToTable("Product");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.DeliveryPrice).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.Name).HasMaxLength(100);
                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<ProductOption>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK__ProductO__3214EC06B63931A5")
                    .IsClustered(false);

                entity.ToTable("ProductOption");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK__User__3214EC06869E490A")
                    .IsClustered(false);

                entity.ToTable("User");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Email).HasMaxLength(320);
                entity.Property(e => e.FirstName).HasMaxLength(100);
                entity.Property(e => e.HashedPassword).HasMaxLength(1024);
                entity.Property(e => e.LastName).HasMaxLength(100);
                entity.Property(e => e.Salt).HasMaxLength(1024);
                entity.Property(e => e.Username).HasMaxLength(128);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
