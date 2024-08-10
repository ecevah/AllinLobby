using AllinLobby.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllinLobby.Entity.Entities
{
    public class Session
    {
        public int SessionId { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; } = null!;
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; } = null!;
        public bool Status { get; set; } = true;
        public Device Device { get; set; }
        public string? DeviceName { get; set; }
        public List<Message> Messages { get; set; } = new List<Message>();
        public DateTime CreateAt { get; set; } = DateTime.UtcNow.AddHours(3);
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow.AddHours(3);
    }
}
