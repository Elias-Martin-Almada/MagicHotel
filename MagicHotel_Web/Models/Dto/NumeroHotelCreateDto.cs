using System.ComponentModel.DataAnnotations;

namespace MagicHotel_Web.Models.Dto
{
    public class NumeroHotelCreateDto
    {
        [Required]
        public int HotelNo { get; set; }

        [Required]
        public int HotelId { get; set; }

        public string DetalleEspecial { get; set; }
    }
}
