using AllinLobby.DTO.DTOs.LocationDtos;
using AllinLobby.Entity.Entities;
using AutoMapper;

namespace AllinLobby.Api.Mapping
{
    public class LocationMapping : Profile
    {
        public LocationMapping()
        {
            CreateMap<CreateLocationDto, Location>().ReverseMap();
            CreateMap<UpdateLocationDto, Location>().ReverseMap();
        }
    }
}
