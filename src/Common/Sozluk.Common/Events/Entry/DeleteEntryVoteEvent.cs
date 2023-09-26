using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Common.Events.Entry
{
    public class DeleteEntryVoteEvent
    {
        public Guid EntryId { get; set; }
        public Guid UserId { get; set; }
    }
}
