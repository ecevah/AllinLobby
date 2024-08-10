using AllinLobby.Entity.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllinLobby.DTO.DTOs.FoodDtos
{
    public class CreateFoodDto
    {
        public int HotelId { get; set; }
        public int FoodCategoryId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public IFormFile Photo { get; set; } = null!;
    }
}
