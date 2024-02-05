﻿using static MagicHotel_Utilidad.DS;

namespace MagicHotel_Web.Models
{
    public class APIRequest
    {
        public APITipo APITipo { get; set; } = APITipo.GET;

        public string Url { get; set; }

        public object Datos { get; set; }
    }
}