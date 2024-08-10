﻿using AllinLobby.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllinLobby.DTO.DTOs.PackageDtos
{
    public class UpdatePackageDto
    {
        public int PackageId { get; set; }
        public int HotelId { get; set; }
        public bool RoomService { get; set; } = false;
        public bool Density { get; set; } = false;
        public bool CleanRoom { get; set; } = false;
        public bool Events { get; set; } = false;
        public bool Complaints { get; set; } = false;
        public bool Comment { get; set; } = false;
        public bool ChatBot { get; set; } = false;
        public bool Directions { get; set; } = false;
        public bool CRM { get; set; } = false;
    }
}
