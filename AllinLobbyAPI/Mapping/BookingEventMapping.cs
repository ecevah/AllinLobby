using AllinLobby.DTO.DTOs.BookingEventDtos;
using AllinLobby.Entity.Entities;
using AutoMapper;

namespace AllinLobby.Api.Mapping
{
    public class BookingEventMapping : Profile
    {
        public BookingEventMapping()
        {
            CreateMap<CreateBookingEventDto, BookingEvent>().ReverseMap();
            CreateMap<UpdateBookingEventDto, BookingEvent>().ReverseMap();
        }
    }
}
