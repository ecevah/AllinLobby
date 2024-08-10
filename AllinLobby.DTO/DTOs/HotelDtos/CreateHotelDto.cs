using AllinLobby.Entity.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllinLobby.DTO.DTOs.HotelDtos
{
    public class CreateHotelDto
    {
        public string Name { get; set; } = null!;
        public int Phone { get; set; }
        public string Address { get; set; } = null!;
        public Country Country { get; set; }
        public bool Status { get; set; }
        public HotelStar Star { get; set; } = HotelStar.OneStar;
        public int AdsScore { get; set; } = 0;
        public string DestinyID { get; set; } = null!;
        public IFormFile Photo { get; set; } = null!;
    }
}
