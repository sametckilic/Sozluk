using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sozluk.Api.Application.Features.Queries.GetEntries;
using Sozluk.Api.Application.Features.Queries.GetMainPagesEntries;
using Sozluk.Common.Models.RequestModels.Entry;
using Sozluk.Common.Models.RequestModels.EntryComment;

namespace Sozluk.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntryController : BaseController
    {
        private readonly IMediator mediator;

        public EntryController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetEntries([FromQuery] GetEntriesQuery query)
        {
            var entries = await mediator.Send(query);

            return Ok(entries);
        }

        [HttpGet]
        [Route("MainPageEntries")]
        public async Task<IActionResult> GetMainPageEntries(int page, int pageSize)
        {
            var entries = await mediator.Send(new GetMainPageEntryQuery(UserId, page, pageSize));

            return Ok(entries);
        }


        [HttpPost]
        [Route("CreateEntry")]
        public async Task<IActionResult> CreateEntry([FromBody] CreateEntryCommand command)
        {
            if (command.CreatedById.HasValue)
                command.CreatedById = UserId;

            var result = await mediator.Send(command);

            return Ok(result);
        }

        [HttpPost]
        [Route("CreateEntryComment")]
        public async Task<IActionResult> CreateEntryComment([FromBody] CreateEntryCommentCommand command)
        {
            if (command.CreatedById.HasValue)
                command.CreatedById = UserId;

            var result = await mediator.Send(command);

            return Ok(result);
        }

    }
}
