
using Crud_Domain.Models;

namespace Crud_Domain
{
    // This interface defines the contract for interacting with
    // a product repository. It includes methods for retrieving
    // products, getting a product by ID, creating, updating,
    // and deleting products, as well as checking if a product
    // with a specific email and date exists. The `SaveAsync`
    // method is used to persist changes to the underlying data store.
    public interface IProductRepository
    {
        Task<List<Product>> GetProductsAsync();
        Task<Product> GetByIdAsync(long id);
        Task CreateProductAsync(Product entity);
        Task UpdateProductAsync(Product entity);
        Task DeleteProductAsync(long id);
        Task<bool> ExistAsync(string email, DateTime date, long? id = null);
        Task SaveAsync();
    }
}
