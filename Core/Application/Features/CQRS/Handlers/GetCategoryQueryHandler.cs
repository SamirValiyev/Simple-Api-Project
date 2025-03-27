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
    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQueryRequest, CategoryDTO>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        public GetCategoryQueryHandler(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryDTO> Handle(GetCategoryQueryRequest request, CancellationToken cancellationToken)
        {
            var category =await _categoryRepository.GetByFilterAsync(x => x.Id == request.Id);
       
            return _mapper.Map<CategoryDTO>(category);
        }
    }
}
