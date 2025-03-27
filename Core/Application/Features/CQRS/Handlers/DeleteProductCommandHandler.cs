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
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            var deletedEntity = await _productRepository.GetByIdAsync(request.Id);
            if (deletedEntity != null)
            {
                await _productRepository.DeleteAsync(deletedEntity);
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
    }
}
