using Application.Features.CQRS.Commands.Create;
using Application.Features.CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register(RegisterUserCommandRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(UserQueryRequest request)
        {
           var dto= await _mediator.Send(request);

            if (dto.IsExist)
            {
                return Created("", "Token yarat");
            }
            else
            {
                return BadRequest("Istifadeci adi ve ya sifre tapilmadi");
            }
        }

    }
}
