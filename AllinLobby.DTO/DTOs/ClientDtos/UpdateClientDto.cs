using AllinLobby.Entity.Entities;
using AllinLobby.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllinLobby.DTO.DTOs.ClientDtos
{
    public class UpdateClientDto
    {
        public int ClientId { get; set; }
        public int SubscriptionId { get; set; }
        public string Name { get; set; } = null!;
        public string PersonalIdentification { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string Phone { get; set; } = null!;
        public string Address { get; set; } = null!;
        public Sex Sex { get; set; }
        public bool Status { get; set; } = true;
        public bool IsBanned { get; set; } = false;
    }
}
