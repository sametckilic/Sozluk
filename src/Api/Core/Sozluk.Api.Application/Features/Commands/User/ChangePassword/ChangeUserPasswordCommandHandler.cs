using MediatR;
using Sozluk.Api.Application.Interfaces.Repositories;
using Sozluk.Common.Infrastructure;
using Sozluk.Common.Infrastructure.Exeptions;
using Sozluk.Common.Models.RequestModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Api.Application.Features.Commands.User.ChangePassword
{
    public class ChangeUserPasswordCommandHandler : IRequestHandler<ChangeUserPasswordCommand, bool>
    {
        private readonly IUserRepository userRepository;

        public ChangeUserPasswordCommandHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<bool> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            if (!request.UserId.HasValue)
                throw new ArgumentNullException(nameof(request.UserId));

            var dbUser = await userRepository.GetByIdAsync(request.UserId.Value);

            if (dbUser == null)
                throw new DatabaseValidationException("User not found!");

            var encryptedOldPassword = PasswordEncrypter.Encrypt(request.OldPassword);
            if (dbUser.Password != encryptedOldPassword)
                throw new DatabaseValidationException("Old password wrong!");

            var encryptedNewPassword = PasswordEncrypter.Encrypt(request.NewPassword);

            dbUser.Password = encryptedNewPassword;

            await userRepository.UpdateAsync(dbUser);

            return true;
        }
    }
}
