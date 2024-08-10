using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllinLobby.Entity.Entities
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; } = null!;
        public int RoomId { get; set; }
        public Room Room { get; set; } = null!;
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; } = null!;
        public DateTime EntryDate { get; set; }
        public DateTime ExitTime { get; set; }
        public DateTime BookingEntryDate { get; set; }
        public DateTime BookingExitTime { get;set; }
        public bool Status { get; set; } = true;
        public decimal Price { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<Guest> Guests { get; set; } = new List<Guest>();
        public List<Order> Orders { get; set; } = new List<Order>();
        public List<Session> Sessions { get; set; } = new List<Session>();
        public DateTime CreateAt { get; set; } = DateTime.UtcNow.AddHours(3);
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow.AddHours(3);
    }
}
