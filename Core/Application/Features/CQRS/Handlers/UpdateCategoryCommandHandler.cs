using Application.Features.CQRS.Commands.Update;
using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CQRS.Handlers
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommandRequest>
    {
        private readonly ICategoryRepository _categoryRepository;
        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task Handle(UpdateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            await _categoryRepository.UpdateAsync(new Domain.Category
            {
                Id=request.Id,
                Definition=request.Definition
            });
        }
    }
}
