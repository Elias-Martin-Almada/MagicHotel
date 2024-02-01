using MagicHotel_Web.Models.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MagicHotel_Web.Models.ViewModel
{
    public class NumeroHotelUpdateViewModel
    {
        public NumeroHotelUpdateViewModel()
        {
            NumeroHotel = new NumeroHoteUpdatelDto(); 
        }

        public NumeroHoteUpdatelDto NumeroHotel { get; set; }
        public IEnumerable<SelectListItem> HotelList { get; set; }
    }
}
