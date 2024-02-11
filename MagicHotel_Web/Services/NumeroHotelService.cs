using MagicHotel_Utilidad;
using MagicHotel_Web.Models;
using MagicHotel_Web.Models.Dto;
using static MagicHotel_Utilidad.DS;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using MagicHotel_Web.Services.IServices;

namespace MagicHotel_Web.Services
{
    public class NumeroHotelService : BaseService, INumeroHotelService
    {
        public readonly IHttpClientFactory _httpClient;
        private string _hotelUrl;
        public NumeroHotelService(IHttpClientFactory httpClient, IConfiguration configuration) : base(httpClient)
        {
            _httpClient = httpClient;
            _hotelUrl = configuration.GetValue<string>("ServiceUrls:API_URL");
        }

        public Task<T> Actualizar<T>(NumeroHoteUpdatelDto dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = APITipo.PUT,
                Datos = dto,
                Url = _hotelUrl + "/api/NumeroHotel/" + dto.HotelNo,
                Token = token
            });
        }

        public Task<T> Crear<T>(NumeroHotelCreateDto dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = APITipo.POST,
                Datos = dto,
                Url = _hotelUrl + "/api/NumeroHotel",
                Token = token
            });
        }

        public Task<T> Obtener<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = APITipo.GET,
                Url = _hotelUrl + "/api/NumeroHotel/" + id,
                Token = token
            });
        }

        public Task<T> ObtenerTodos<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = APITipo.GET,
                Url = _hotelUrl + "/api/NumeroHotel",
                Token = token
            });
        }

        public Task<T> Remover<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = APITipo.DELETE,
                Url = _hotelUrl + "/api/NumeroHotel/" + id,
                Token = token
            });
        }
    }
}
