using AutoMapper;
using MagicHotel_Utilidad;
using MagicHotel_Web.Models;
using MagicHotel_Web.Models.Dto;
using MagicHotel_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace MagicHotel_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHotelService _hotelService;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IHotelService hotelService, IMapper mapper)
        {
            _logger = logger;
            _hotelService = hotelService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            List<HotelDto> hotelList = new();

            var response = await _hotelService.ObtenerTodos<APIResponse>(HttpContext.Session.GetString(DS.SessionToken));

            if(response != null && response.IsExitoso)
            {
                hotelList = JsonConvert.DeserializeObject<List<HotelDto>>(Convert.ToString(response.Resultado));
            }

            return View(hotelList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
