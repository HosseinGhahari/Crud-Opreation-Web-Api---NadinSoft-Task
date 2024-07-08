using Crud_Application.CQRS.Commands;
using Crud_Application.CQRS.Queries;
using Crud_Application_Contracts.CQRS.Commands;
using Crud_Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Crud_Project.Controllers
{
    public class MainController : Controller
    {
        // It takes an instance of IMediator as a parameter,
        // which is used for handling commands and queries
        // in the CQRS pattern.
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
        [HttpGet,Authorize]
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


        // Creates a new product. Validates input data and user
        // authorization. Sends a CreateProductCommand to the Mediator.
        // Returns an HTTP 200 OK status with the command response if
        // successful; otherwise, returns a 400 Bad Request status.
        [HttpPost,Authorize]
        [Route("CreateProduct")]
        public async Task<IActionResult> CreateProduct(CreateProductCommand.Command command)
        {
            if (command == null)
            {
                return BadRequest("Inputs Are Null");
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            command.UserId = userId;
            var response = await _mediator.Send(command);

            if (response != null)
            {
                return Ok(response);
            }

            return BadRequest("Inputs Are Null");
        }


        // This HTTP PUT method updates a product with a given ID.
        // It sends an UpdateProductCommand containing the product details
        // to the Mediator, which handles the update process. The method
        // then checks whether the current user is the owner of the product.
        // If not, it returns a 403 Forbidden status; otherwise, it returns
        // an HTTP 200 OK status along with the response from the command handler.
        [HttpPut,Authorize]
        [Route("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(UpdateProductCommand.Command command)
        {
            var product = await _mediator.Send(new GetProductByIdQuery.Query {Id = command.Id});
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            command.UserId = userId;

            if(Convert.ToString(product.Product.UserId) != userId)
            {
                return Forbid();
            }

            var response = await _mediator.Send(command);
            return Ok(response);
        }


        // Deletes a product with a given ID. just like update
        // opreation Validates ownership and sends a DeleteProductCommand
        // to the Mediator Returns an HTTP 200 OK status with the command response.
        [HttpPut,Authorize]
        [Route("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(DeleteProductCommand.Command command, long id )
        {
            if (command.Id != id)
            {
                return BadRequest("The product ID does not match.");
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            command.UserId = userId;
            var product = await _mediator.Send(new GetProductByIdQuery.Query { Id = id });

            if (Convert.ToString(product.Product.UserId) != userId)
            {
                return Forbid();
            }

            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
