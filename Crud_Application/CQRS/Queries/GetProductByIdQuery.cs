using Crud_Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud_Application.CQRS.Queries
{
    // `GetProductByIdQuery` is a CQRS query for retrieving a
    // product by its ID. The `Query` class takes the product ID.
    // The `Handler` fetches the product by ID from the `IProductRepository`
    // and returns it in the `Response` class. If the product is not found,
    // the `Product` property in the `Response` will be null.
    public class GetProductByIdQuery
    {
        public class Query : IRequest<Response>
        {
            [Required]
            public long Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Response>
        {
            private readonly IProductRepository _productRepository;
            public Handler(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var product = await _productRepository.GetByIdAsync(request.Id);
                var response = new Response
                {
                    Product = product
                };

                return response;
            }
        }

        public class Response
        {
        public Product Product { get; set; }
    }
    }
}
