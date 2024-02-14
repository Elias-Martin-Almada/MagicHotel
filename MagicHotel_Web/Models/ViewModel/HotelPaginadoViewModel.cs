using MagicHotel_Web.Models.Dto;

namespace MagicHotel_Web.Models.ViewModel
{
    public class HotelPaginadoViewModel
    {
        public int PageNumber { get; set; }

        public int TotalPaginas { get; set; }

        public string Previo { get; set; } = "disabled";

        public string Siguiente { get; set; } = "";

        public IEnumerable<HotelDto> HotelList { get; set; }
    }
}
