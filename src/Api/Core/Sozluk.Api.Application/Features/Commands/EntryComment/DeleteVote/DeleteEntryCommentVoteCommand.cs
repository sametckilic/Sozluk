using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Api.Application.Features.Commands.EntryComment.DeleteVote
{
    public class DeleteEntryCommentVoteCommand : IRequest<bool>
    {
        public DeleteEntryCommentVoteCommand(Guid entryCommentId, Guid createdBy)
        {
            EntryCommentId = entryCommentId;
            CreatedBy = createdBy;
        }

        public Guid EntryCommentId { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
