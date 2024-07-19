using Crud_Domain;
using Crud_Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Crud_Application_Contracts.CQRS.Commands
{
    // This code defines a CQRS command for creating a product.
    // The `CreateProductCommand` class contains a `Command` with
    // properties representing product details (name, production date,
    // phone, email, and availability). The `Handler` processes the command,
    // creates a new product, and saves it using the provided `IProductRepository`
    // . The response includes the generated product ID.
    public class CreateProductCommand
    {
        public class Command : IRequest<Response>
        {
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
                var product = Product.Create(
                    request.Name,
                    request.ProduceDate,
                    request.ManufacturePhone,
                    request.ManufactureEmail,
                    request.IsAvailable,
                    request.UserId              
                );

                await _productRepository.CreateProductAsync(product);

                var response = new Response
                {
                    Id = product.Id,
                };

                return response; 
            }
        }

        public class Response
        {
            public long Id { get; set; }
        }
    }
}
