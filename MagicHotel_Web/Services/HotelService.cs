using MagicHotel_Utilidad;
using MagicHotel_Web.Models;
using MagicHotel_Web.Models.Dto;
using static MagicHotel_Utilidad.DS;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using MagicHotel_Web.Services.IServices;

namespace MagicHotel_Web.Services
{
    public class HotelService : BaseService, IHotelService
    {
        public readonly IHttpClientFactory _httpClient;
        private string _hotelUrl;
        public HotelService(IHttpClientFactory httpClient, IConfiguration configuration) : base(httpClient)
        {
            _httpClient = httpClient;
            _hotelUrl = configuration.GetValue<string>("ServiceUrls:API_URL");
        }

        public Task<T> Actualizar<T>(HotelUpdateDto dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = APITipo.PUT,
                Datos = dto,
                Url = _hotelUrl + "/api/Hotel/" + dto.Id,
                Token = token
            });
        }

        public Task<T> Crear<T>(HotelCreateDto dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = APITipo.POST,
                Datos = dto,
                Url = _hotelUrl + "/api/Hotel",
                Token = token
            });
        }

        public Task<T> Obtener<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = APITipo.GET,
                Url = _hotelUrl + "/api/Hotel/" + id,
                Token = token
            });
        }

        public Task<T> ObtenerTodos<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = APITipo.GET,
                Url = _hotelUrl + "/api/Hotel",
                Token = token
            });
        }

        public Task<T> Remover<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = APITipo.DELETE,
                Url = _hotelUrl + "/api/Hotel/" + id,
                Token = token
            });
        }
    }
}
