using AllinLobby.Entity.Entities;
using AllinLobby.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllinLobby.DTO.DTOs.RoomDtos
{
    public class UpdateRoomDto
    {
        public int RoomId { get; set; }
        public int HotelId { get; set; }
        public RoomType RoomType { get; set; }
        public decimal Prce { get; set; }
        public int Capacity { get; set; }
        public bool Status { get; set; }
    }
}
