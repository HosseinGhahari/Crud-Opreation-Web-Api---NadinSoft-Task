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
    // `UpdateProductCommand` is a CQRS command for updating
    // product details. The `Command` class takes the product
    // ID and new details. The `Handler` fetches the product by
    // ID, updates its details if found, and saves the changes.
    // If the product is not found, it returns an error message.
    // The result of the operation, including a success flag and
    // a message, is returned in the `Response` class.
    public class UpdateProductCommand
    {
        public class Command : IRequest<Response>
        {
            [Required]
            public long Id { get; set; }
            [Required]
            public string Name { get; set; }
            [Required]
            public DateTime ProduceDate { get; set; }
            [Required]
            public int ManufacturePhone { get; set; }
            [Required]
            public string ManufactureEmail { get; set; }
            [Required]
            public bool IsAvailable { get; set; }
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
                    return new Response { IsSuccessful = false, Message = "Product not found" };
                }
             
                product.Name = request.Name;
                product.ManufacturePhone = request.ManufacturePhone;
                product.ManufactureEmail = request.ManufactureEmail;
                product.ProduceDate = request.ProduceDate;
                product.IsAvailable = request.IsAvailable;

                try
                {
                    await _productRepository.UpdateProductAsync(product);
                    return new Response { IsSuccessful = true, Message = "Product updated successfully" };
                }
                catch (Exception ex)
                {
                    return new Response { IsSuccessful = false, Message = ex.Message };
                }
            }
        }

        public class Response
        {
            public bool IsSuccessful { get; set; }
            public string Message { get; set; }
        }

    }
}
