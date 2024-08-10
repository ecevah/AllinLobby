using AllinLobby.DTO.DTOs.PackageDtos;
using AllinLobby.Entity.Entities;
using AutoMapper;

namespace AllinLobby.Api.Mapping
{
    public class PackageMapping : Profile
    {
        public PackageMapping()
        {
            CreateMap<CreatePackageDto, Package>().ReverseMap();
            CreateMap<UpdatePackageDto, Package>().ReverseMap();
        }
    }
}
