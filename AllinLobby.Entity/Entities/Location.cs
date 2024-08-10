using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllinLobby.Entity.Entities
{
    public class Location
    {
        public int LocationId { get; set; }
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string PhotoPath {  get; set; } = null!;
        public DateTime Open {  get; set; }
        public DateTime Close { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow.AddHours(3);
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow.AddHours(3);
    }
}
