using ProductAPI.Data;
using ProductAPI.Entities;
using ProductAPI.Commands;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ProductAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _context;

        public ProductRepository(IProductContext productContext)
        {
            _context = (ProductContext)productContext ?? throw new ArgumentNullException(nameof(productContext));
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context
                            .Products
                            
                            .ToListAsync();
        }

        public async Task<IEnumerable<Product>> SortProducts(string column, SortDirection direction)
        {
            if(direction == SortDirection.Asc)
            {
                return await _context
                            .Products
                            .OrderBy(p => EF.Property<string>(p, column))
                            .ToListAsync();
            }

            return await _context
                            .Products
                            .OrderByDescending(p => EF.Property<string>(p, column))
                            .ToListAsync();
        }

        public async Task<Product> GetProduct(int id)
        {
            return await _context
                            .Products
                            .FindAsync(id)
                            .AsTask();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            return await _context
                          .Products
                          .Where(p => p.Name.Contains(name))
                          .ToListAsync();
        }

        public async Task<IEnumerable<Product>> FilterProductByColor(string color)
        {
            return await _context
                          .Products
                          .Where(p => p.Color == color)
                          .ToListAsync();
        }

        public async Task<IEnumerable<Product>> FilterProductByBranch(string branch)
        {
            return await _context
                          .Products
                          .Where(p => p.Branch == branch)
                          .ToListAsync();
        }
        public async Task Create(Product product)
        {
            await _context.Products.AddAsync(product);
            _context.SaveChanges();
        }

        public async Task<bool> Update(Product product)
        {
            var updateResult = _context
                                        .Products
                                        .Update(product);
            var state = updateResult.State;
            await _context.SaveChangesAsync();
            return state == EntityState.Modified;
        }

        public async Task<bool> Delete(int id)
        {
            var product = await this.GetProduct(id);
            var deleteResult = _context
                                                .Products
                                                .Remove(product);
            var state = deleteResult.State;
            await _context.SaveChangesAsync();
            
            return state == EntityState.Deleted;
        }
    }
}