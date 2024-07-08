using Crud_Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud_Application.CQRS.Commands
{
    // DeleteProductCommand` is a CQRS command for deleting products
    // The `Command` class takes the product ID.The `Handler` fetches
    // the product by ID and deletes it if found, otherwise returns an
    // error message.The result of the operation is returned in the `Response` class.

    public class DeleteProductCommand
    {
        public class Command : IRequest<Response>
        {
            [Required]
            public long Id { get; set; }
            [NonSerialized]
            public string UserId;
        }

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IProductRepository _productRepository;
            public Handler(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var product = await _productRepository.GetByIdAsync(request.Id);
                if (product == null)
                {
                    return new Response { Message = "Product Id not found" };
                }

                await _productRepository.DeleteProductAsync(product.Id);
                return new Response { Message = "Product Removed" };
            }
        }

        public class Response
        {
            public string Message { get; set; }
        }
    }
}
