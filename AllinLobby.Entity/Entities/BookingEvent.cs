using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllinLobby.Entity.Entities
{
    public class BookingEvent
    {
        public int BookingEventId { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; } = null!;
        public int ClientId { get; set; }
        public Client Client { get; set; } = null!;
        public bool Status { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow.AddHours(3);
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow.AddHours(3);
    }
}
