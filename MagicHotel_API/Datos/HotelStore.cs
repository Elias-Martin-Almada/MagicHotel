using MagicHotel_API.Modelos.Dto;

namespace MagicHotel_API.Datos
{
    public static class HotelStore // Almacenamiento de Registros.
    {
        public static List<HotelDto> hotelList = new List<HotelDto>
        {
            new HotelDto{Id=1, Nombre="Vista a la Piscina", Ocupantes=3, MetrosCuadrados=50},
            new HotelDto{Id=2, Nombre="Vista a la Playa", Ocupantes=4, MetrosCuadrados=80},
        };

    }
}
