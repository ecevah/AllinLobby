using AllinLobby.DTO.DTOs.PackageDtos;
using AllinLobby.Entity.Entities;
using AutoMapper;

namespace AllinLobby.Api.Mapping
{
    public class PaymentMapping : Profile
    {
        public PaymentMapping()
        {
            CreateMap<CreatePackageDto, Payment>().ReverseMap();
            CreateMap<UpdatePackageDto, Payment>().ReverseMap();
        }
    }
}
