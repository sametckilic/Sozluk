using AutoMapper;
using MediatR;
using Sozluk.Api.Application.Interfaces.Repositories;
using Sozluk.Common;
using Sozluk.Common.Events.Entry;
using Sozluk.Common.Events.EntryComment;
using Sozluk.Common.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Api.Application.Features.Commands.Entry.CreateFav
{
    public class CreateEntryFavCommandHandler : IRequestHandler<CreateEntryFavCommand, bool>
    {
        public Task<bool> Handle(CreateEntryFavCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName: SozlukContants.FavExchangeName,
                                               exchangedType: SozlukContants.DefaultExchangeType,
                                               queueName: SozlukContants.CreateEntryFavQueueName,
                                               obj: new CreateEntryFavEvent()
                                               {

                                                   EntryId = request.EntryId,
                                                   CreatedBy = request.UserId
                                               });
            return Task.FromResult(true);
        }
    }
}
