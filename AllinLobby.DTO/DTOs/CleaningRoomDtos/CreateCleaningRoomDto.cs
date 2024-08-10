using AllinLobby.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllinLobby.DTO.DTOs.CleaningRoomDtos
{
    public class CreateCleaningRoomDto
    {
        public int HotelId { get; set; }
        public int RoomId { get; set; }
        public DateTime TimeStamp { get; set; }
        public DateTime StartAvaible { get; set; }
        public DateTime FinishAvaible { get; set; }
        public bool DisturbMode { get; set; } = false;
    }
}
