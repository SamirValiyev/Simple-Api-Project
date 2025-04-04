using Application.Features.CQRS.Commands.Create;
using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CQRS.Handlers
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategroyCommandRequest>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task Handle(CreateCategroyCommandRequest request, CancellationToken cancellationToken)
        {
            await _categoryRepository.AddAsync(new Domain.Category
            {
                Definition = request.Definition,
                Name = request.Name

            });
        }
    }
}
