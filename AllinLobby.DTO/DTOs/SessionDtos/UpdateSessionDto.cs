using AllinLobby.Entity.Entities;
using AllinLobby.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllinLobby.DTO.DTOs.SessionDtos
{
    public class UpdateSessionDto
    {
        public int SessionId { get; set; }
        public int ClientId { get; set; }
        public int ReservationId { get; set; }
        public bool Status { get; set; } = true;
        public Device Device { get; set; }
        public string? DeviceName { get; set; }
    }
}
