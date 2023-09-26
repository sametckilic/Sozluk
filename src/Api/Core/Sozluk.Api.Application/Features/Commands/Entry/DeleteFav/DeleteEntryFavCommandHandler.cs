using MediatR;
using Sozluk.Api.Domain.Models;
using Sozluk.Common;
using Sozluk.Common.Events.Entry;
using Sozluk.Common.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Api.Application.Features.Commands.Entry.DeleteFav
{
    public class DeleteEntryFavCommandHandler : IRequestHandler<DeleteEntryFavCommand, bool>
    {
        public Task<bool> Handle(DeleteEntryFavCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName: SozlukContants.FavExchangeName,
                                               exchangedType: SozlukContants.DefaultExchangeType,
                                               queueName: SozlukContants.DeleteEntryFavQueueName,
                                               obj: new DeleteEntryFavEvent()
                                               {
                                                   EntryId = request.EntryId,
                                                   UserId = request.UserId,
                                               });
            return Task.FromResult(true);
        }
    }
}
