using AutoMapper;
using RestLib.Infrastructure.Entities;
using RestLib.Infrastructure.Models.V1;
using System;

namespace RestLib.Infrastructure.Profiles
{
    public class BoardsProfile : Profile
    {
        public BoardsProfile()
        {
            CreateMap<Board, ResponseBoardDto>()
                .ForMember(dest => dest.LastActivityOn,
                opt => opt.MapFrom(src => src.UpdatedOn == DateTime.MinValue ? src.CreatedOn : src.UpdatedOn))
                .ForMember(dest => dest.LastServerResetOn,
                opt => opt.MapFrom(src => src.CreatedOn));

            //CreateMap<PagedList<Board>, PagedList<ResponseBoardDto>>();
        }
    }
}
