using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllinLobby.Entity.Entities
{
    public class Event
    {
        public int EventId { get; set; }
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; } = null!;
        public int LocationId { get; set; } 
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int? Capacity { get; set; }
        public int CategoryId { get; set; }
        public string PhotoPath { get; set; } = null!;
        public List<BookingEvent> BookingEvents { get; set; } = new List<BookingEvent>();
        public DateTime CreateAt { get; set; } = DateTime.UtcNow.AddHours(3);
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow.AddHours(3);

    }
}
