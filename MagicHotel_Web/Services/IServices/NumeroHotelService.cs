using MagicHotel_Utilidad;
using MagicHotel_Web.Models;
using MagicHotel_Web.Models.Dto;
using static MagicHotel_Utilidad.DS;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

namespace MagicHotel_Web.Services.IServices
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

        public Task<T> Actualizar<T>(NumeroHoteUpdatelDto dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.PUT,
                Datos = dto,
                Url = _hotelUrl + "/api/NumeroHotel/" + dto.HotelNo
            });
        }

        public Task<T> Crear<T>(NumeroHotelCreateDto dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.POST,
                Datos = dto,
                Url= _hotelUrl + "/api/NumeroHotel"
            });
        }

        public Task<T> Obtener<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.GET,
                Url = _hotelUrl + "/api/NumeroHotel/" + id
            });
        }

        public Task<T> ObtenerTodos<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.GET,
                Url = _hotelUrl + "/api/NumeroHotel"
            });
        }

        public Task<T> Remover<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.DELETE,
                Url = _hotelUrl + "/api/NumeroHotel/" + id
            });
        }
    }
}
