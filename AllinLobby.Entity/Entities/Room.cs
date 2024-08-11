using AllinLobby.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllinLobby.Entity.Entities
{
    public class Room
    {
        public int RoomId { get; set; }
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; } = null!;
        public RoomType RoomType { get; set; }
        public decimal Prce { get; set; }
        public int Capacity { get; set; }
        public bool Status { get; set; }
        public List<CleaningRoom> CleaningRooms { get; set; } = new List<CleaningRoom>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<Complaint> Complaints { get; set; } = new List<Complaint>();
        public List<Reservation> Reservations { get; set; } = new List<Reservation>();
        public DateTime CreateAt { get; set; } = DateTime.UtcNow.AddHours(3);
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow.AddHours(3);
    }
}
