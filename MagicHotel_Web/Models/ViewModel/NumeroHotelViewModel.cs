using MagicHotel_Web.Models.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MagicHotel_Web.Models.ViewModel
{
    public class NumeroHotelViewModel
    {
        public NumeroHotelViewModel()
        {
            NumeroHotel = new NumeroHotelCreateDto(); 
        }

        public NumeroHotelCreateDto NumeroHotel { get; set; }
        public IEnumerable<SelectListItem> HotelList { get; set; }
    }
}
