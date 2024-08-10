using AllinLobby.Entity.Entities;
using AllinLobby.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllinLobby.DTO.DTOs.SubscriptionDtos
{
    public class CreateSubscriptionDto
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public SubsriberType SubscriberType { get; set; }
    }
}
