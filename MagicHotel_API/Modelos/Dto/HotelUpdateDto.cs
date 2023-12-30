using System.ComponentModel.DataAnnotations;

namespace MagicHotel_API.Modelos.Dto
{
    public class HotelUpdateDto // Objeto de Transferencia de Datos.
    {
        [Required]
        public int Id { get; set; }

        [Required]     // Validaciones para el ModelState
        [MaxLength(30)]
        public string Nombre { get; set; }
        public string Detalle { get; set; }
        [Required]
        public double Tarifa { get; set; }
        [Required]
        public int Ocupantes { get; set; }
        [Required]
        public int MetrosCuadrados { get; set; }
        [Required]
        public string ImagenUrl { get; set; }
        public string Amenidad { get; set; }
    }
}
