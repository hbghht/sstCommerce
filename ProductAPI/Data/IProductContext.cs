using ProductAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace ProductAPI.Data
{
    public interface IProductContext
    {
        public DbSet<Product>? Products { get; set;}
    }
}
