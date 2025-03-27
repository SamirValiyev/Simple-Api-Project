using Application.DTO;
using Application.Features.CQRS.Commands.Create;
using Application.Interfaces;
using AutoMapper;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CQRS.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            await _productRepository.AddAsync(new Product
            {
                Name = request.Name,
                Stock = request.Stock,
                CategoryId = request.CategoryId,
                Price = request.Price
            });
            

        }
    }
}
