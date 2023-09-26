using MediatR;
using Sozluk.Common;
using Sozluk.Common.Events.EntryComment;
using Sozluk.Common.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Api.Application.Features.Commands.EntryComment.CreateFav
{
    public class CreateEntryCommentFavCommandHandler : IRequestHandler<CreateEntryCommentFavCommand, bool>
    {

        public async Task<bool> Handle(CreateEntryCommentFavCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName: SozlukContants.FavExchangeName,
                                               exchangedType: SozlukContants.DefaultExchangeType,
                                               queueName: SozlukContants.CreateEntryCommentFavQueueName,
                                               obj: new CreateEntryCommentFavEvent()
                                               {
                                                   EntryCommentId = request.EntryCommendId,
                                                   CreatedBy = request.UserId
                                               });

            return await Task.FromResult(true);
        }
    }
}
