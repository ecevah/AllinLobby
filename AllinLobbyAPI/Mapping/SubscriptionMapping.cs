using AllinLobby.DTO.DTOs.SubscriptionDtos;
using AllinLobby.Entity.Entities;
using AutoMapper;

namespace AllinLobby.Api.Mapping
{
    public class SubcriptionMapping : Profile
    {
        public SubcriptionMapping()
        {
            CreateMap<CreateSubscriptionDto, Subscription>().ReverseMap();
            CreateMap<UpdateSubscriptionDto, Subscription>().ReverseMap();
        }
    }
}
