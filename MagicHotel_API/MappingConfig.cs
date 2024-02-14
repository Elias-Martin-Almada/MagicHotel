using AutoMapper;
using MagicHotel_API.Modelos;
using MagicHotel_API.Modelos.Dto;

namespace MagicHotel_API
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Hotel, HotelDto>();
            CreateMap<HotelDto, Hotel>();

            CreateMap<Hotel, HotelCreateDto>().ReverseMap();
            CreateMap<Hotel, HotelUpdateDto>().ReverseMap();

            CreateMap<NumeroHotel, NumeroHotelDto>().ReverseMap();
            CreateMap<NumeroHotel, NumeroHotelCreateDto>().ReverseMap();
            CreateMap<NumeroHotel, NumeroHoteUpdatelDto>().ReverseMap();
            CreateMap<UsuarioAplicacion, UsuarioDto>().ReverseMap();
        }
    }
}
