using AutoMapper;
using RestLib.Infrastructure.Entities;
using RestLib.Infrastructure.Models.V1.Messages;

namespace RestLib.Infrastructure.Profiles
{
    public class MessagesProfile : Profile
    {
        public MessagesProfile()
        {
            CreateMap<Message, ResponseMessageDto>();

            CreateMap<RequestMessageDto, Message>();

            CreateMap<UpdateMessageDto, Message>();
        }
    }
}
