using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Api.Application.Features.Commands.EntryComment.CreateFav
{
    public class CreateEntryCommentFavCommand : IRequest<bool>
    {
        public Guid EntryCommendId { get; set; }
        public Guid UserId { get; set; }

        public CreateEntryCommentFavCommand(Guid EntryCommendId, Guid UserId)
        {
            this.EntryCommendId = EntryCommendId;
            this.UserId = UserId;
        }

    }
}
