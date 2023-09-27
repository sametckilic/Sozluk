using AutoMapper;
using MediatR;
using Sozluk.Api.Application.Interfaces.Repositories;
using Sozluk.Common.Models.Pages;
using Sozluk.Common.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Api.Application.Features.Queries.GetMainPagesEntries
{
    public class GetMainPageEntryQuery : BasePagedQuery, IRequest<PagedViewModel<GetEntryDetailViewModel>>
    {


        public GetMainPageEntryQuery(Guid? userId, int page, int pageSize) : base(page, pageSize)
        {
            UserId = userId;
        }
        public Guid? UserId { get; set; }
    }
}
