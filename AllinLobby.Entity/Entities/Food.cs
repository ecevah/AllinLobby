using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllinLobby.Entity.Entities
{
    public class Food
    {
        public int FoodId { get; set; }
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; } = null!;
        public int FoodCategoryId {  get; set; }
        public FoodCategory FoodCategory { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price {  get; set; }
        public int Stock { get; set; }
        public string PhotoPath { get; set; } = null!;
        public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
        public DateTime CreateAt { get; set; } = DateTime.UtcNow.AddHours(3);
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow.AddHours(3);
    }
}
