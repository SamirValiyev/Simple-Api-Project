using Application.Features.CQRS.Queries;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Categories()
        {
            var categories=await _mediator.Send(new GetAllCategoryQueryRequest());
            return categories == null ? NotFound() : Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Category(int id)
        {
            var category = await _mediator.Send(new GetCategoryQueryRequest(id));
            return category == null ? NotFound() : Ok(category);
        }

    }
}
