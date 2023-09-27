using MediatR;
using Sozluk.Common.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Api.Application.Features.Queries.GetQueries
{
    public class GetEntriesQuery : IRequest<List<GetEntriesViewModel>>
    {

        public bool TodaysEntries { get; set; }
        public int Count { get; set; } = 100;
    }

}
