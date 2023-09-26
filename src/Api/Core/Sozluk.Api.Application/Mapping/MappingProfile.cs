using AutoMapper;
using Sozluk.Api.Domain.Models;
using Sozluk.Common.Models.Queries;
using Sozluk.Common.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Api.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            // user maps

            CreateMap<User, LoginUserViewModel>()
                .ReverseMap();

            CreateMap<CreateUserCommand, User>();

            CreateMap<UpdateUserCommand, User>();

            //entries map

            CreateMap<CreateEntryCommand, Entry>()
                .ReverseMap();
        }
    }
}
