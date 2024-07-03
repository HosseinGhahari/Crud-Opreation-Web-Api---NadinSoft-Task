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
                    request.IsAvailable
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
