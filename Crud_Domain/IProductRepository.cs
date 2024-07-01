using Crud_Application.Contracts.Product;

namespace Crud_Domain
{
    public interface IProductRepository
    {
        // IProductService defines the contract for interacting with product-related operations.
        // It includes methods for retrieving products, creating, updating, and deleting products,
        // checking product existence, and saving changes. Implementations of this interface
        // provide the actual logic for these operations.
        List<ProductViewModel> GetProducts();
        Product GetById(long id);
        void CreateProduct(AddProduct entity);
        void UpdateProduct(UpdateProduct entity);
        void DeleteProduct(long id);
        bool Exist(string email, DateTime date, long? id = null);
        void Save();
    }
}
