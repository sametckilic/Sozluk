using MediatR;
using Microsoft.EntityFrameworkCore;
using Sozluk.Api.Application.Interfaces.Repositories;
using Sozluk.Common.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Api.Application.Features.Queries.SearchBySubject
{
    public class SearchEntryQueryHandler : IRequestHandler<SearchEntryQuery, List<SearchEntryViewModel>>
    {
        private readonly IEntryRepository entryRepository;

        public SearchEntryQueryHandler(IEntryRepository entryRepository)
        {
            this.entryRepository = entryRepository;
        }

        public async Task<List<SearchEntryViewModel>> Handle(SearchEntryQuery request, CancellationToken cancellationToken)
        {
            if (request.SearchText.Length > 3)
            {
                var result = entryRepository.Get(i => EF.Functions.Like(i.Subject, $"%{request.SearchText}%"))
                    .Select(i => new SearchEntryViewModel()
                    {
                        Id = i.Id,
                        Subject = i.Subject
                    });
                return await result.ToListAsync(cancellationToken);
            }
            else
            {
                return null;
            }



        }
    }
}
