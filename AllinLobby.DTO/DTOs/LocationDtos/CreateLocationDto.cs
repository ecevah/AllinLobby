using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllinLobby.DTO.DTOs.LocationDtos
{
    public class CreateLocationDto
    {
        public int HotelID { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public IFormFile Photo { get; set; } = null!;
        public DateTime Open { get; set; }
        public DateTime Close { get; set; }
    }
}
