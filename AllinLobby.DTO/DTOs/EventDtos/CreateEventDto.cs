using AllinLobby.Entity.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllinLobby.DTO.DTOs.EventDtos
{
    public class CreateEventDto
    {
        public int HotelId { get; set; }
        public int LocationId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int? Capacity { get; set; }
        public int CategoryId { get; set; }
        public IFormFile Photo { get; set; } = null!;
    }
}
