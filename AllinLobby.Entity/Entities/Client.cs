using AllinLobby.Entity.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllinLobby.Entity.Entities
{
    public class Client : IdentityUser<int>
    {
        public int ClientId { get; set; }
        public int SubscriptionId { get; set; }
        public Subscription Subscription { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int PersonalIdentification { get; set; }
        public DateTime BirthDate { get; set; }
        public int Phone { get; set; }
        public string Address { get; set; } = null!;
        public Sex Sex { get; set; }
        public bool Status { get; set; } = true;
        public bool IsBanned { get; set; } = false;
        public List<BookingEvent> BookingEvents { get; set; } = new List<BookingEvent>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<Complaint> Complaints { get; set; } = new List<Complaint>();
        public List<Guest> Guests { get; set; } = new List<Guest>();
        public List<Order> Orders { get; set; } = new List<Order>();
        public List<Reservation> Reservations { get; set; } = new List<Reservation>();
        public List<Session> Sessions { get; set; } = new List<Session>();
        public DateTime CreateAt { get; set; } = DateTime.UtcNow.AddHours(3);
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow.AddHours(3);
    }
}
