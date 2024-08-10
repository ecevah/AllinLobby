﻿using AllinLobby.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllinLobby.DTO.DTOs.GuestDtos
{
    public class UpdateGuestDto
    {
        public int FoodCategoryId { get; set; }
        public int HotelId { get; set; }
        public string Name { get; set; } = null!;
    }
}