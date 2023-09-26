using MediatR;
using Sozluk.Common;
using Sozluk.Common.Events.Entry;
using Sozluk.Common.Infrastructure;
using Sozluk.Common.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Api.Application.Features.Commands.Entry.CreateVote
{
    public class CreateEntryVoteCommandHandler : IRequestHandler<CreateEntryVoteCommand, bool>
    {

        public async Task<bool> Handle(CreateEntryVoteCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName: SozlukContants.VoteExchangeName,
                                        exchangedType: SozlukContants.DefaultExchangeType,
                                        queueName: SozlukContants.CreateEntryVoteQueueName,
                                        obj: new CreateEntryVoteEvent()
                                        {
                                            EntryId = request.EntryId,
                                            CreatedBy = request.CreatedBy,
                                            VoteType = request.VoteType,
                                        });

            return await Task.FromResult(true);
        }
    }
}
