using MediatR;
using Sozluk.Common;
using Sozluk.Common.Events.EntryComment;
using Sozluk.Common.Infrastructure;

namespace Sozluk.Api.Application.Features.Commands.EntryComment.DeleteVote
{
    public class DeleteEntryCommentVoteCommandHandler : IRequestHandler<DeleteEntryCommentVoteCommand, bool>
    {
        public async Task<bool> Handle(DeleteEntryCommentVoteCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName: SozlukContants.FavExchangeName,
                                               exchangedType: SozlukContants.DefaultExchangeType,
                                               queueName: SozlukContants.DeleteEntryCommentVoteQueueName,
                                               obj: new DeleteEntryCommentVoteEvent()
                                               {
                                                   EntryCommentId = request.EntryCommentId,
                                                   UserId = request.CreatedBy
                                               });
            return await Task.FromResult(true);
        }
    }
}
