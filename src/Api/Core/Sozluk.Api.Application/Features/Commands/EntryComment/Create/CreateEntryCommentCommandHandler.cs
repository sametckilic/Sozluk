using AutoMapper;
using MediatR;
using Sozluk.Api.Application.Interfaces.Repositories;
using Sozluk.Common.Models.RequestModels.Entry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Api.Application.Features.Commands.EntryComment.Create
{
    public class CreateEntryCommentCommandHandler : IRequestHandler<CreateEntryCommand, Guid>
    {
        private readonly IEntryCommentRepository entryCommentRepository;
        private readonly IMapper mapper;

        public CreateEntryCommentCommandHandler(IEntryCommentRepository entryCommentRepository, IMapper mapper)
        {
            this.entryCommentRepository = entryCommentRepository;
            this.mapper = mapper;
        }

        public async Task<Guid> Handle(CreateEntryCommand request, CancellationToken cancellationToken)
        {
            var dbEntryComment = mapper.Map<Domain.Models.EntryComment>(request);

            await entryCommentRepository.AddAsync(dbEntryComment);

            return dbEntryComment.Id;
        }
    }
}
