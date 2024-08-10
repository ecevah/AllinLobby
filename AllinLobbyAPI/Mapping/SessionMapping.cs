using AllinLobby.DTO.DTOs.SessionDtos;
using AllinLobby.Entity.Entities;
using AutoMapper;

namespace AllinLobby.Api.Mapping
{
    public class SessionMapping : Profile
    {
        public SessionMapping()
        {
            CreateMap<CreateSessionDto, Session>().ReverseMap();
            CreateMap<UpdateSessionDto, Session>().ReverseMap();
        }
    }
}
