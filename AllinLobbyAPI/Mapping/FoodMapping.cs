using AllinLobby.DTO.DTOs.FoodDtos;
using AllinLobby.Entity.Entities;
using AutoMapper;

namespace AllinLobby.Api.Mapping
{
    public class FoodMapping : Profile
    {
        public FoodMapping()
        {
            CreateMap<CreateFoodDto, Food>().ReverseMap();
            CreateMap<UpdateFoodDto, Food>().ReverseMap();
        }
    }
}
