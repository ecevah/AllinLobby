using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllinLobby.Entity.Entities
{
    public class OrderDetail
    {
        public int OrderDetailId {  get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;
        public int FoodId { get; set; }
        public Food Food { get; set; } = null!;
        public int Quentity { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow.AddHours(3);
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow.AddHours(3);
    }
}
