using Crud_Application.Contracts.Product;
using Crud_Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud_Application
{
    public class ProductService : IProductService
    {
        // Constructor injection: Initializes the service with a product repository.

        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // Creates a new product using the provided entity.
        public void CreateProduct(AddProduct entity)
        {
            _productRepository.CreateProduct(entity);
        }

        // Deletes a product based on its ID.
        public void DeleteProduct(long id)
        {
            _productRepository.DeleteProduct(id);
        }

        // Retrieves a list of product view models.
        public List<ProductViewModel> GetProducts()
        {
            return _productRepository.GetProducts();
        }

        // Updates an existing product using the provided entity.
        public void UpdateProduct(UpdateProduct entity)
        {
             _productRepository.UpdateProduct(entity);
        }
    }
}
