using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllinLobby.Entity.Entities
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; } = null!;
        public int RoomId { get; set; }
        public Room Room { get; set; } = null!;
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; } = null!;
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; } = null!;
        public int Rating {  get; set; }
        public bool Status {  get; set; }
        public string CommentText { get; set; } = null!;
        public DateTime CreateAt { get; set; } = DateTime.UtcNow.AddHours(3);
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow.AddHours(3);
    }
}
