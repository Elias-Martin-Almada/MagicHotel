using System.ComponentModel.DataAnnotations;

namespace MagicHotel_API.Modelos.Dto
{
    public class HotelCreateDto // Objeto de Transferencia de Datos.
    {
        [Required]     // Validaciones para el ModelState
        [MaxLength(30)]
        public string Nombre { get; set; }
        public string Detalle { get; set; }
        [Required]
        public double Tarifa { get; set; }
        public int Ocupantes { get; set; }
        public int MetrosCuadrados { get; set; }
        public string ImagenUrl { get; set; }
        public string Amenidad { get; set; }
    }
}
