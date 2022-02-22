using ProductAPI.Entities;
using ProductAPI.Settings;
using Microsoft.EntityFrameworkCore;

namespace ProductAPI.Data
{
  public class ProductContext : DbContext, IProductContext
  {
    #region DefineLoggerFactory
    public static readonly ILoggerFactory MyLoggerFactory
        = LoggerFactory.Create(builder => builder.AddConsole()) ;
    #endregion

    ProductDatabaseSettings settings;
    public ProductContext(ProductDatabaseSettings settings) : base()
    {
      this.settings = settings;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(settings.ConnectionString);
        optionsBuilder.UseLoggerFactory(MyLoggerFactory);
    }

    public DbSet<Product>? Products { get; set;}
  }
}