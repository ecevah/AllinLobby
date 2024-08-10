using AllinLobby.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllinLobby.DTO.DTOs.CommentDtos
{
    public class CreateCommentDto
    {
        public int ClientId { get; set; }
        public int RoomId { get; set; }
        public int HotelId { get; set; }
        public int ReservationId { get; set; }
        public int Rating { get; set; }
        public bool Status { get; set; }
        public string CommentText { get; set; } = null!;
    }
}
