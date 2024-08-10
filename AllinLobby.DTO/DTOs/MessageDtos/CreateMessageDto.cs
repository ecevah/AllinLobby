using AllinLobby.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllinLobby.DTO.DTOs.MessageDtos
{
    public class CreateMessageDto
    {
        public int SessionId { get; set; }
        public string Text { get; set; } = null!;
        public string ResponseMessage { get; set; } = null!;
    }
}
