
namespace Crud_Domain
{
    public interface IProductRepository
    {
        // IProductService defines the contract for interacting with product-related operations.
        // It includes methods for retrieving products, creating, updating, and deleting products,
        // checking product existence, and saving changes. Implementations of this interface
        // provide the actual logic for these operations.
        List<Product> GetProducts();
        Product GetById(long id);
        Task CreateProductAsync(Product entity);
        void UpdateProduct(Product entity);
        void DeleteProduct(long id);
        bool Exist(string email, DateTime date, long? id = null);
        Task SaveAsync();
    }
}
