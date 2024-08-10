using AllinLobby.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllinLobby.DTO.DTOs.ReservationDtos
{
    public class CreateReservationDto
    {
        public int ClientId { get; set; }
        public int RoomId { get; set; }
        public int HotelId { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime ExitTime { get; set; }
        public DateTime BookingEntryDate { get; set; }
        public DateTime BookingExitTime { get; set; }
        public bool Status { get; set; } = true;
        public decimal Price { get; set; }
    }
}
