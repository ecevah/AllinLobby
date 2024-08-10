using AllinLobby.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllinLobby.DTO.DTOs.ComplaintDtos
{
    public class UpdateComplaintDto
    {
        public int ComplaintID { get; set; }
        public int HotelId { get; set; }
        public int ClientId { get; set; }
        public int RoomId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool Status { get; set; }
        public string Priorty { get; set; } = null!;
        public DateTime ResponseDate { get; set; }
        public string? ResponseText { get; set; }
        public string? Photo { get; set; }
    }
}
