using Application.DTO;
using Application.Features.CQRS.Queries;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CQRS.Handlers
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQueryRequest, ProductDTO>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductDTO> Handle(GetProductQueryRequest request, CancellationToken cancellationToken)
        {
           var product = await _productRepository.GetByFilterAsync(x=>x.Id == request.Id);
            return _mapper.Map<ProductDTO>(product);   
        }
    }
}
