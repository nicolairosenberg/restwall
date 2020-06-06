using AutoMapper;
using RestLib.Infrastructure.Entities;
using RestLib.Infrastructure.Helpers;
using RestLib.Infrastructure.Models.V1;
using System;

namespace RestLib.Infrastructure.Profiles
{
    public class TopicsProfile : Profile
    {
        public TopicsProfile()
        {
            CreateMap<Topic, ResponseTopicDto>()
                .ForMember(dest => dest.LastActivityOn,
                opt => opt.MapFrom(src => src.UpdatedOn == DateTime.MinValue || src.UpdatedOn == null? src.CreatedOn : src.UpdatedOn));

            CreateMap<RequestTopicDto, Topic>()
                .ForMember(dest => dest.Messages, opt => opt.MapFrom(src => src.Messages));//.ReverseMap();

            CreateMap<UpdateTopicDto, Topic>();

            CreateMap<PagedList<Topic>, PagedList<ResponseTopicDto>>();
        }
    }
}
