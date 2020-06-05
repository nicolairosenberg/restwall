using AutoMapper;
using RestLib.Infrastructure.Entities;
using RestLib.Infrastructure.Models.V1;
using System;

namespace RestLib.Infrastructure.Profiles
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<Message, ResponseMessageDto>();

            CreateMap<RequestMessageDto, Message>();
        }
    }
}
