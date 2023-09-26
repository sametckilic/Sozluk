using MediatR;
using Sozluk.Common;
using Sozluk.Common.Events.Entry;
using Sozluk.Common.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Api.Application.Features.Commands.Entry.DeleteVote
{
    internal class DeleteEntryVoteCommandHandler : IRequestHandler<DeleteEntryVoteCommand, bool>
    {

        public Task<bool> Handle(DeleteEntryVoteCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName: SozlukContants.VoteExchangeName,
                                    exchangedType: SozlukContants.DefaultExchangeType,
                                    queueName: SozlukContants.DeleteEntryVoteQueueName,
                                    obj: new DeleteEntryVoteEvent()
                                    {
                                        EntryId = request.EntryId,
                                        UserId = request.UserId,
                                    });

            return Task.FromResult(true);
        }
    }
}
