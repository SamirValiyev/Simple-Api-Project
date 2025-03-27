using Application.Features.CQRS.Commands.Update;
using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CQRS.Handlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var updatedProduct = _productRepository.GetByIdAsync(request.Id);
            if (updatedProduct != null)
            {
                updatedProduct.Result.Name = request.Name;
                updatedProduct.Result.Stock = request.Stock;
                updatedProduct.Result.Price = request.Price;
                updatedProduct.Result.CategoryId = request.CategoryId;
                await _productRepository.UpdateAsync(updatedProduct.Result);
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
    }
}
