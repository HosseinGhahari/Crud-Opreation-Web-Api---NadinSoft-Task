using Crud_Application.CQRS.Commands;
using Crud_Application.CQRS.Queries;
using Crud_Application_Contracts.CQRS.Commands;
using Crud_Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Crud_Project.Controllers
{
    public class MainController : Controller
    {
        private readonly IMediator _mediator;
        public MainController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // HTTP GET endpoint for retrieving a list of products.
        // Retrieve a list of products using the injected IProductService.
        // Return the products as an HTTP 200 (OK) response.
        [HttpGet]
        [Route("GetProducts")]
        public async Task<IActionResult> Index()
        {
            var products = await _mediator.Send(new GetProductsQuery.Query());
            return Ok(products);
        }


        // HTTP GET endpoint for retrieving a product by its ID.
        // Call the GetById method from the injected IProductService.
        // If the product is not found, return an HTTP 404 (Not Found) response.
        // Otherwise, return the product as an HTTP 200 (OK) response.
        [HttpGet]
        [Route("GetProductById")]
        public async Task <IActionResult> GetById(int id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery.Query { Id = id});
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
        public async Task<IActionResult> CreateProduct(CreateProductCommand.Command command)
        {
            if (command == null)
            {
                return BadRequest("Inputs Are Null");
            }

            var response = await _mediator.Send(command);

            if (response != null)
            {
                return Ok(response);
            }

            return BadRequest("Inputs Are Null");
        }

        // HTTP PUT endpoint for updating an existing product.
        // Call the UpdateProduct method from the injected IProductService.
        // This method updates the product data in the database or another data store.
        // Return an HTTP 200 (OK) response.
        [HttpPut]
        [Route("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(UpdateProductCommand.Command command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        // HTTP PUT endpoint for deleting a product by its ID.
        // Call the DeleteProduct method from the injected IProductService.
        // This method removes the product from the database or another data store.
        // Return an HTTP 200 (OK) response to indicate successful deletion.
/*        [HttpPut]
        [Route("DeleteProduct")]
        public IActionResult DeleteProduct(int id)
        {
            _productService.DeleteProduct(id);
            return Ok();
        }*/
    }
}
