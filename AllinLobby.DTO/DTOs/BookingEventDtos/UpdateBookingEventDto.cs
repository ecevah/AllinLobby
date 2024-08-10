using AllinLobby.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllinLobby.DTO.DTOs.BookingEventDtos
{
    public class UpdateBookingEventDto
    {
        public int BookingEventId { get; set; }
        public int EventId { get; set; }
        public int ClientId { get; set; }
        public bool Status { get; set; }
    }
}
