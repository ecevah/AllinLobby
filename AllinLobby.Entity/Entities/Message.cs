using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllinLobby.Entity.Entities
{
    public class Message
    {
        public int MessageId { get; set; }
        public int SessionId { get; set; }
        public Session Session { get; set; } = null!;
        public string Text { get; set; } = null!;
        public string ResponseMessage { get; set; } = null!;
        public DateTime CreateAt { get; set; } = DateTime.UtcNow.AddHours(3);
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow.AddHours(3);
    }
}
