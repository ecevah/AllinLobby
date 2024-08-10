using AllinLobby.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllinLobby.Entity.Entities
{
    public class Hotel
    {
        public int HotelId { get; set; }
        public string Name { get; set; } = null!;
        public int Phone { get; set; }
        public string Address { get; set; } = null!;
        public Country Country { get; set; }
        public bool Status { get; set; }
        public HotelStar Star { get; set; } = HotelStar.OneStar;
        public int AdsScore { get; set; } = 0;
        public string DestinyID { get; set; } = null!;
        public string PhotoPath { get; set; } = null!;
        public List<CleaningRoom> CleaningRooms { get; set; } = new List<CleaningRoom>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<Complaint> Complaints { get; set; } = new List<Complaint>();
        public List<Event> Events { get; set; } = new List<Event>();
        public List<Food> Foods { get; set; } = new List<Food>();
        public List<Hotel> Hotels { get; set; } = new List<Hotel>();
        public List<Order> Orders { get; set; } = new List<Order>();
        public List<Package> Packages { get; set; } = new List<Package>();
        public List<Reservation> Reservations { get; set; } = new List<Reservation>();
        public List<Room> Rooms { get; set; } = new List<Room>();
        public DateTime CreateAt { get; set; } = DateTime.UtcNow.AddHours(3);
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow.AddHours(3);
    }
}
