using ProductAPI.Entities;
using ProductAPI.Commands;

namespace ProductAPI.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> SortProducts(string column, SortDirection direction);

        Task<Product> GetProduct(int id);

        Task<IEnumerable<Product>> GetProductByName(string name);

        Task<IEnumerable<Product>> FilterProductByColor(string color);
        
        Task<IEnumerable<Product>> FilterProductByBranch(string branch);
    }
}