using Application.Features.CQRS.Commands.Delete;
using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CQRS.Handlers
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommandRequest>
    {
        private readonly ICategoryRepository _categoryRepository;

        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task Handle(DeleteCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var deletedCategory=await _categoryRepository.GetByIdAsync(request.Id);
            if (deletedCategory != null)
            {
                await _categoryRepository.DeleteAsync(deletedCategory);
            }
           
        }
    }
}
