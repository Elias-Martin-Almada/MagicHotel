using MagicHotel_Web.Models;

namespace MagicHotel_Web.Services.IServices
{
    public interface IBaseService
    {
        public APIResponse responseModel {  get; set; }

        Task<T> SendAsync<T>(APIRequest apiRequest);
    }
}
