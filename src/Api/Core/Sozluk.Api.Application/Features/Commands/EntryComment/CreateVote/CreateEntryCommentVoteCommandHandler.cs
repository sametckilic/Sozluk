using MediatR;
using Sozluk.Common;
using Sozluk.Common.Events.EntryComment;
using Sozluk.Common.Infrastructure;
using Sozluk.Common.Models.RequestModels.EntryComment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Api.Application.Features.Commands.EntryComment.CreateVote
{
    public class CreateEntryCommentVoteCommandHandler : IRequestHandler<CreateEntryCommentVoteCommand, bool>
    {
        public async Task<bool> Handle(CreateEntryCommentVoteCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName: SozlukContants.VoteExchangeName,
                                               exchangedType: SozlukContants.DefaultExchangeType,
                                               queueName: SozlukContants.CreateEntryCommentVoteQueueName,
                                               obj: new CreateEntryCommentVoteEvent()
                                               {
                                                   CreatedBy = request.CreatedBy,
                                                   EntryCommentId = request.EntryCommentId,
                                                   VoteType = request.VoteType,
                                               });
            return await Task.FromResult(true);
        }
    }
}
