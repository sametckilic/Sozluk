using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sozluk.Common.Models.RequestModels.User;

namespace Sozluk.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator mediator;

        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var res = await mediator.Send(command);

            return Ok(res);
        }
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
        {
            var guid = await mediator.Send(command);

            return Ok(guid);
        }
        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateUserCommand command)
        {
            var guid = await mediator.Send(command);

            return Ok(guid);
        }
    }
}
