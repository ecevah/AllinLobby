using AllinLobby.DTO.DTOs.OrderDetailDtos;
using AllinLobby.Entity.Entities;
using AllinLobby.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllinLobby.DTO.DTOs.OrderDtos
{
    public class CreateOrderDto
    {
        public int ClientId { get; set; }
        public int HotelId { get; set; }
        public int ReservationId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime DeliveryTime { get; set; }
        public List<CreateOrderDetailDto> OrderDetails { get; set; } = new List<CreateOrderDetailDto>();
    }
}
