using AllinLobby.DTO.DTOs.RoomDtos;
using AllinLobby.Entity.Entities;
using AutoMapper;

namespace AllinLobby.Api.Mapping
{
    public class RoomMapping : Profile
    {
        public RoomMapping()
        {
            CreateMap<CreateRoomDto, Room>().ReverseMap();
            CreateMap<UpdateRoomDto, Room>().ReverseMap();
        }
    }
}
