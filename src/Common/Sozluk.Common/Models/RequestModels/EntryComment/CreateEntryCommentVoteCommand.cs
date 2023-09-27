using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Common.Models.RequestModels.EntryComment
{
    public class CreateEntryCommentVoteCommand : IRequest<bool>
    {
        public Guid EntryCommentId { get; set; }
        public VoteType VoteType { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
