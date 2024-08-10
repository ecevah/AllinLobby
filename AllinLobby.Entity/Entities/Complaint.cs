using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllinLobby.Entity.Entities
{
    public class Complaint
    {
        public int ComplaintID { get; set; }
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; } = null!;
        public int ClientId { get; set; }
        public Client Client { get; set; } = null!;
        public int RoomId { get; set; }
        public Room Room { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool Status { get; set; }
        public string Priorty { get; set; } = null!;
        public DateTime ResponseDate { get; set; }
        public string? ResponseText { get; set; }
        public string? Photo { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow.AddHours(3);
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow.AddHours(3);


    }
}
