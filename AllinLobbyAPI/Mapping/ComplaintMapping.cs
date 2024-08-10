using AllinLobby.DTO.DTOs.ComplaintDtos;
using AllinLobby.Entity.Entities;
using AutoMapper;

namespace AllinLobby.Api.Mapping
{
    public class ComplaintMapping : Profile
    {
        public ComplaintMapping()
        {
            CreateMap<CreateComplaintDto, Complaint>().ReverseMap();
            CreateMap<UpdateComplaintDto, Complaint>().ReverseMap();
        }
    }
}
