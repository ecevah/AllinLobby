using AllinLobby.DTO.DTOs.HotelDtos;
using AllinLobby.Entity.Entities;
using AutoMapper;

namespace AllinLobby.Api.Mapping
{
    public class HotelMapping : Profile
    {
        public HotelMapping() 
        {
            CreateMap<CreateHotelDto, Hotel>().ReverseMap();
            CreateMap<UpdateHotelDto, Hotel>().ReverseMap();
        }
    }
}
