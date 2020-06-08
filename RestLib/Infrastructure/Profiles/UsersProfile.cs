using AutoMapper;
using RestLib.Infrastructure.Entities;
using RestLib.Infrastructure.Models.V1.Users;

namespace RestLib.Infrastructure.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<User, ResponseUserDto>()
                .ForMember(dest => dest.JoinedOn,
                opt => opt.MapFrom(src => src.CreatedOn));

            CreateMap<RequestUserDto, User>();

            CreateMap<UpdateUserDto, User>();

            CreateMap<User, ResponseUserLinksDto>()
               .ForMember(dest => dest.JoinedOn,
               opt => opt.MapFrom(src => src.CreatedOn));

            CreateMap<ResponseUserDto, ResponseUserLinksDto>();
        }
    }
}
