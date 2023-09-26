using AutoMapper;
using MediatR;
using Sozluk.Api.Application.Interfaces.Repositories;
using Sozluk.Api.Domain.Models;
using Sozluk.Common.Events.User;
using Sozluk.Common.Infrastructure;
using Sozluk.Common;
using Sozluk.Common.Infrastructure.Exeptions;
using Sozluk.Common.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Api.Application.Features.Commands.User.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Guid>
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;

        public UpdateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
        }

        public async Task<Guid> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var dbUser = await userRepository.GetByIdAsync(request.Id);

            if (dbUser != null)
                throw new DatabaseValidationException("User not found!");

            var dbEmailAddress = dbUser.Email;
            var emailChanged = string.CompareOrdinal(dbEmailAddress, request.Email) != 0;


            mapper.Map(request, dbUser);



            var rows = await userRepository.UpdateAsync(dbUser);


            //Email changed/created

            if (emailChanged && rows > 0)
            {
                var @event = new UserEmailChangeEvent()
                {
                    OldEmailAddress = null,
                    NewEmailAddress = dbUser.Email
                };
                QueueFactory.SendMessageToExchange(exchangeName: SozlukContants.UserExchangeName,
                                                   exchangedType: SozlukContants.DefaultExchangeType,
                                                   queueName: SozlukContants.UserEmailChangedQueueName,
                                                   obj: @event);
                dbUser.EmailConfirmed = false;
                await userRepository.UpdateAsync(dbUser);
            }

            return dbUser.Id;
        }

    }
}
