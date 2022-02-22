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

        public async Task<IEnumerable<Product>> SortProducts(string column, SortDirection direction)
        {
            if(_context.Products != null )
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
            
            return new List<Product>();            
        }

        public async Task<IEnumerable<Product>> FilterProductByColor(string color)
        {
            if(_context.Products != null )
            {
                return await _context
                            .Products
                            .Where(p => p.Color == color)
                            .ToListAsync();
            }

            return new List<Product>();   
        }

        public async Task<IEnumerable<Product>> FilterProductByBranch(string branch)
        {
            if(_context.Products != null )
            {
                return await _context
                          .Products
                          .Where(p => p.Branch == branch)
                          .ToListAsync();
            }
            return new List<Product>();
        }

        public async Task<Product> GetProduct(int id)
        {
            if(_context.Products != null )
            {
                var findResult = await _context
                            .Products
                            .FindAsync(id);

                if(findResult != null)
                {
                    return findResult;
                }
            }
            return new Product();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            if(_context.Products != null )
            {
                return await _context
                          .Products
                          .Where(p => p.Name.Contains(name))
                          .ToListAsync();
            }

            return new List<Product>();
        }
    }
}