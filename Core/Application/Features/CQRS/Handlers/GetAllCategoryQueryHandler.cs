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
    public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQueryRequest, List<CategoryDTO>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public GetAllCategoryQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }


        public async Task<List<CategoryDTO>> Handle(GetAllCategoryQueryRequest request, CancellationToken cancellationToken)
        {
            var categories=await _categoryRepository.GetAllAsync();
            return _mapper.Map<List<CategoryDTO>>(categories);
        }
    }
}
