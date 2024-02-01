using MagicHotel_Web.Models.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MagicHotel_Web.Models.ViewModel
{
    public class NumeroHotelDeleteViewModel
    {
        public NumeroHotelDeleteViewModel()
        {
            NumeroHotel = new NumeroHotelDto(); 
        }

        public NumeroHotelDto NumeroHotel { get; set; }
        public IEnumerable<SelectListItem> HotelList { get; set; }
    }
}
