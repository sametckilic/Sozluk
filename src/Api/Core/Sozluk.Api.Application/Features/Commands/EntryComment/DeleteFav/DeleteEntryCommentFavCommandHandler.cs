using MediatR;
using Sozluk.Api.Application.Features.Commands.Entry.DeleteFav;
using Sozluk.Common.Events.Entry;
using Sozluk.Common.Infrastructure;
using Sozluk.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sozluk.Common.Events.EntryComment;

namespace Sozluk.Api.Application.Features.Commands.EntryComment.DeleteFav
{
    public class DeleteEntryCommentFavCommandHandler : IRequestHandler<DeleteEntryCommentFavCommand, bool>
    {
        public async Task<bool> Handle(DeleteEntryCommentFavCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName: SozlukContants.FavExchangeName,
                                               exchangedType: SozlukContants.DefaultExchangeType,
                                               queueName: SozlukContants.DeleteEntryCommentFavQueueName,
                                               obj: new DeleteEntryCommentFavEvent()
                                               {
                                                   EntryCommentId = request.EntryCommentId,
                                                   UserId = request.UserId,
                                               });
            return await Task.FromResult(true);
        }
    }
}
