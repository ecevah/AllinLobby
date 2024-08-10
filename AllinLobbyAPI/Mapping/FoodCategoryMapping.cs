
using AllinLobby.DTO.DTOs.FoodCategoryDtos;
using AllinLobby.Entity.Entities;
using AutoMapper;

namespace AllinLobby.Api.Mapping
{
    public class FoodCategoryMapping : Profile
    {
        public FoodCategoryMapping()
        {
            CreateMap<CreateFoodCategoryDto, FoodCategory>().ReverseMap();
            CreateMap<UpdateFoodCategoryDto, FoodCategory>().ReverseMap();
        }
    }
}
