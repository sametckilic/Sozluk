using Sozluk.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Common.Events.EntryComment
{
    public class CreateEntryCommentVoteEvent
    {
        public Guid EntryCommentId { get; set; }
        public Guid CreatedBy { get; set; }
        public VoteType VoteType { get; set; }


    }
}
