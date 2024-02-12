using AutoMapper;
using MagicHotel_API.Datos;
using MagicHotel_API.Modelos;
using MagicHotel_API.Modelos.Dto;
using MagicHotel_API.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Numerics;

namespace MagicHotel_API.Controllers.v2
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class NumeroHotelController : ControllerBase
    {
        private readonly ILogger<NumeroHotelController> _logger; // Registra info sobre el comportamiento de la API
        private readonly IHotelRepositorio _hotelRepo;         // DbContext para usar datos de la DB
        private readonly INumeroHotelRepositorio _numeroRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public NumeroHotelController(ILogger<NumeroHotelController> logger, IHotelRepositorio hotelRepo,
                                                                            INumeroHotelRepositorio numeroRepo, IMapper mapper)
        {
            _logger = logger;
            _hotelRepo = hotelRepo;
            _numeroRepo = numeroRepo;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "valor1", "valor2" };
        }

    }   

}
