using AutoMapper;
using MagicHotel_Web.Models.Dto;

namespace MagicHotel_Web
{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        { 
            CreateMap<HotelDto, HotelCreateDto>().ReverseMap();
            CreateMap<HotelDto, HotelUpdateDto>().ReverseMap();

            CreateMap<NumeroHotelDto, NumeroHotelCreateDto>().ReverseMap();
            CreateMap<NumeroHotelDto, NumeroHoteUpdatelDto>().ReverseMap();
        }
    }
}
