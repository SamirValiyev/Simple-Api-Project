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
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductQueryRequest, List<GetAllProductsDTO>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public GetAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }


        public async Task<List<GetAllProductsDTO>> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync();
            return  _mapper.Map<List<GetAllProductsDTO>>(products);

        }
    }
}
