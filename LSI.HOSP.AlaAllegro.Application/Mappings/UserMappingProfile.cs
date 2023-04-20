using AutoMapper;
using LSI.HOSP.AlaAllegro.Application.Users;
using LSI.HOSP.AlaAllegro.Application.Users.Commands;
using LSI.HOSP.AlaAllegro.Application.Users.Queries;
using LSI.HOSP.AlaAllegro.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LSI.HOSP.AlaAllegro.Application.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<CreateUpdateUserCommand, User>()
                .ForMember(e => e.PhoneNumber, opt => opt.MapFrom(src => src.Phone))
                .ForMember(e => e.Password, opt => opt.MapFrom(src => UserHashPassword.HashPassword(src.Password)));

            CreateMap<User, UsersAllViewModel>();            

        }        
    }
}
