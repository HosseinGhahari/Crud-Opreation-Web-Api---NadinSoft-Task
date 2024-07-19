using Crud_Domain;
using Crud_Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud_Application.CQRS.Queries
{

    // This code defines a CQRS query for retrieving a list of products.
    // The `GetProductsQuery` class contains a `Query` with properties
    // representing optional filters (name, production date, phone, email,
    // and availability). The `Handler` processes the query by fetching
    // products from the repository based on the specified filters. The
    // response includes a list of products matching the query criteria.
    public class GetProductsQuery
    {
        public class Query : IRequest<Response>
        {
            public string Name { get; set; }
            public DateTime ProduceDate { get; set; }
            public int ManufacturePhone { get; set; }
            public string ManufactureEmail { get; set; }
            public bool IsAvailable { get; set; }

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
                var product = await _productRepository.GetProductsAsync();
                var response = new Response
                {
                    Products = product
                };

                return response;

            }
        }

        public class Response
        {
            public List<Product> Products { get; set; }
        }

    }
}
