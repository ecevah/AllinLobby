using AllinLobby.DTO.DTOs.CleaningRoomDtos;
using AllinLobby.Entity.Entities;
using AutoMapper;

namespace AllinLobby.Api.Mapping
{
    public class ClenaingRoomMapping : Profile
    {
        public ClenaingRoomMapping()
        {
            CreateMap<CreateCleaningRoomDto, CleaningRoom>().ReverseMap();
            CreateMap<UpdateCleaningRoomDto, CleaningRoom>().ReverseMap();
        }
    }
}
