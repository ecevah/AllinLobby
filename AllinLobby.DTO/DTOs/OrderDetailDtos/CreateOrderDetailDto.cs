using AllinLobby.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllinLobby.DTO.DTOs.OrderDetailDtos
{
    public class CreateOrderDetailDto
    {
        public int FoodId { get; set; }
        public int Quentity { get; set; }
    }
}
