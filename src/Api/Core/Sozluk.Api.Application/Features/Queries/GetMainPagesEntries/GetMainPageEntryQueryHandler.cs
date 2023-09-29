using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sozluk.Api.Application.Interfaces.Repositories;
using Sozluk.Api.Domain.Models;
using Sozluk.Common.Infrastructure.Extensions;
using Sozluk.Common.Models.Pages;
using Sozluk.Common.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Api.Application.Features.Queries.GetMainPagesEntries
{
    public class GetMainPageEntryQueryHandler : IRequestHandler<GetMainPageEntryQuery, PagedViewModel<GetEntryDetailViewModel>>
    {
        private readonly IEntryRepository entryRepository;

        public GetMainPageEntryQueryHandler(IEntryRepository entryRepository)
        {
            this.entryRepository = entryRepository;
        }
        public async Task<PagedViewModel<GetEntryDetailViewModel>> Handle(GetMainPageEntryQuery request, CancellationToken cancellationToken)
        {
            var query = entryRepository.AsQueryable();

            query = query
                .Include(i => i.EntryFavorites)
                .Include(i => i.CreatedBy)
                .Include(i => i.EntryVotes);
            var list = query.Select(i => new GetEntryDetailViewModel()
            {
                Id = i.Id,
                Subject = i.Subject,
                Content = i.Content,
                IsFavorited = request.UserId.HasValue && i.EntryFavorites.Any(j => j.CreatedById == request.UserId),
                FavoritedCount = i.EntryFavorites.Count,
                CreatedDate = i.CreateDate,
                CreatedByUserName = i.CreatedBy.UserName,
                VoteType =
                request.UserId.HasValue && i.EntryVotes.Any(j => j.CreatedById == request.UserId)
                ? i.EntryVotes.FirstOrDefault(j => j.CreatedById == request.UserId).VoteType
                : Common.Models.VoteType.None

            });

            var entries = await list.GetPaged(request.Page, request.PageSize);

            return new PagedViewModel<GetEntryDetailViewModel>(entries.Results, entries.PageInfo);
        }
    }
}
