using Crud_Application.Contracts.Product;
using Crud_Domain;
using Microsoft.AspNetCore.Mvc;

namespace Crud_Project.Controllers
{
    public class MainController : Controller
    {
        // here we injects an instance of IProductService
        // and IProductRepository to interact with product data.
        private readonly IProductService _productService;
        private readonly IProductRepository _productRepository;
        public MainController(IProductService productService , IProductRepository productRepository)
        {
            _productService = productService;
            _productRepository = productRepository;
        }

        // HTTP GET endpoint for retrieving a list of products.
        // Retrieve a list of products using the injected IProductService.
        // Return the products as an HTTP 200 (OK) response.
        [HttpGet]
        [Route("GetProducts")]
        public IActionResult Index()
        {
            var products = _productService.GetProducts();
            return Ok(products);
        }

        // HTTP GET endpoint for retrieving a product by its ID.
        // Call the GetById method from the injected IProductService.
        // If the product is not found, return an HTTP 404 (Not Found) response.
        // Otherwise, return the product as an HTTP 200 (OK) response.
        [HttpGet]
        [Route("GetProductById")]
        public IActionResult GetById(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // HTTP POST endpoint for creating a new product.
        // Check if the input product is not null.
        // Call the CreateProduct method from the injected IProductService.
        // This method adds the product to the database or another data store.
        [HttpPost]
        [Route("CreateProduct")]
        public IActionResult CreateProduct(AddProduct product)
        {
            if (product != null)
            {
                _productService.CreateProduct(product);
                return Ok();
            }

            return BadRequest("Inputs Are Null");

        }

        // HTTP PUT endpoint for updating an existing product.
        // Call the UpdateProduct method from the injected IProductService.
        // This method updates the product data in the database or another data store.
        // Return an HTTP 200 (OK) response.
        [HttpPut]
        [Route("UpdateProduct")]
        public IActionResult UpdateProduct(UpdateProduct product)
        {
            _productService.UpdateProduct(product);
            return Ok();
        }

        // HTTP PUT endpoint for deleting a product by its ID.
        // Call the DeleteProduct method from the injected IProductService.
        // This method removes the product from the database or another data store.
        // Return an HTTP 200 (OK) response to indicate successful deletion.
        [HttpPut]
        [Route("DeleteProduct")]
        public IActionResult DeleteProduct(int id)
        {
            _productService.DeleteProduct(id);
            return Ok();
        }
    }
}
