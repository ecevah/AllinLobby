using AllinLobby.DTO.DTOs.EventDtos;
using AllinLobby.Entity.Entities;
using AutoMapper;

namespace AllinLobby.Api.Mapping
{
    public class EventMapping : Profile
    {
        public EventMapping()
        {
            CreateMap<CreateEventDto, Event>().ReverseMap();
            CreateMap<UpdateEventDto, Event>().ReverseMap();
        }
    }
}
