using AllinLobby.DTO.DTOs.ClientDtos;
using AllinLobby.Entity.Entities;
using AutoMapper;

namespace AllinLobby.Api.Mapping
{
    public class ClientMapping : Profile
    {
        public ClientMapping()
        {
            CreateMap<CreateClientDto, Client>().ReverseMap();
            CreateMap<UpdateClientDto, Client>().ReverseMap();
        }
    }
}
