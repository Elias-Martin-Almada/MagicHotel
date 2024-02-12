using MagicHotel_Utilidad;
using MagicHotel_Web.Models;
using MagicHotel_Web.Models.Dto;
using MagicHotel_Web.Services.IServices;

namespace MagicHotel_Web.Services
{
	public class UsuarioService : BaseService, IUsuarioService
	{
		public readonly IHttpClientFactory _httpClient;
		private string _hotelUrl;

        public UsuarioService(IHttpClientFactory httpClient, IConfiguration configuration) : base(httpClient)
        {
            _httpClient = httpClient;
			_hotelUrl = configuration.GetValue<string>("ServiceUrls:API_URL");
        }

        public Task<T> Login<T>(LoginRequestDto dto)
		{
			return SendAsync<T>(new APIRequest()
			{
				APITipo = DS.APITipo.POST,
				Datos = dto,
				Url = _hotelUrl + "/api/v1/usuario/login"
            });
		}

		public Task<T> Registrar<T>(RegistroRequestDto dto)
		{
			return SendAsync<T>(new APIRequest()
			{
				APITipo = DS.APITipo.POST,
				Datos = dto,
				Url = _hotelUrl + "/api/v1/usuario/registrar"
            });
		}
	}
}
