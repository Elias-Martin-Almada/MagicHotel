using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicHotel_API.Modelos
{
    public class Hotel
    {
        [Key] // Le digo a Entity que Id es clave primaria
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // El id se asigna solo.
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        public string Detalle { get; set; }
        [Required]
        public double Tarifa { get; set; }
        public int Ocupantes { get; set; }
        public int MetrosCuadrados { get; set; }
        public string ImagenUrl { get; set; }
        public string Amenidad { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }
}
