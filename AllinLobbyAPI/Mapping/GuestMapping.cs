using AllinLobby.DTO.DTOs.GuestDtos;
using AllinLobby.Entity.Entities;
using AutoMapper;

namespace AllinLobby.Api.Mapping
{
    public class GuestMapping : Profile
    {
        public GuestMapping()
        {
            CreateMap<CreateGuestDto, Guest>().ReverseMap();
            CreateMap<UpdateGuestDto, Guest>().ReverseMap();
        }
    }
}
