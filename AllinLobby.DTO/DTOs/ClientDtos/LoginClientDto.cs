using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllinLobby.DTO.DTOs.ClientDtos
{
    public class LoginClientDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
