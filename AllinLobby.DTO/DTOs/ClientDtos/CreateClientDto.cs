﻿using AllinLobby.Entity.Entities;
using AllinLobby.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllinLobby.DTO.DTOs.ClientDtos
{
    public class CreateClientDto
    {
        public string Email { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
        public int SubscriptionId { get; set; }
        public string Name { get; set; } = null!;
        public double PersonalIdentification { get; set; }
        public DateTime BirthDate { get; set; }
        public double Phone { get; set; }
        public string Address { get; set; } = null!;
        public Sex Sex { get; set; }
    }
}
