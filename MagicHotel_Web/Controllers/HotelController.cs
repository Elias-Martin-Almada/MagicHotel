using AutoMapper;
using MagicHotel_Utilidad;
using MagicHotel_Web.Models;
using MagicHotel_Web.Models.Dto;
using MagicHotel_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MagicHotel_Web.Controllers
{
    public class HotelController : Controller
    {
        private readonly IHotelService _hotelService;
        private readonly IMapper _mapper;

        public HotelController(IHotelService hotelService, IMapper mapper)
        {
            _hotelService = hotelService;
            _mapper = mapper;
        }

        public async Task<IActionResult> IndexHotel()
        {
            List<HotelDto> hotelList = new();

            var response = await _hotelService.ObtenerTodos<APIResponse>(HttpContext.Session.GetString(DS.SessionToken));

            if(response != null && response.IsExitoso)
            {
                hotelList = JsonConvert.DeserializeObject<List<HotelDto>>(Convert.ToString(response.Resultado));
            } 

            return View(hotelList);
        }

        //Get 1) Llama a la Vista
        public async Task<IActionResult> CrearHotel()
        {
            return View();
        }
        // 2) Manda la informacion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearHotel(HotelCreateDto modelo)
        {
            if(ModelState.IsValid)
            {
                var response = await _hotelService.Crear<APIResponse>(modelo, HttpContext.Session.GetString(DS.SessionToken));

                if(response != null && response.IsExitoso)
                {
                    TempData["exitoso"] = "Hotel Creado Exitosamente";
                    return RedirectToAction(nameof(IndexHotel));
                }
            }
            return View(modelo);
        }

        public async Task<IActionResult> ActualizarHotel(int hotelId)
        {
            var response = await _hotelService.Obtener<APIResponse>(hotelId, HttpContext.Session.GetString(DS.SessionToken));

            if(response != null && response.IsExitoso)
            {
                HotelDto model = JsonConvert.DeserializeObject<HotelDto>(Convert.ToString(response.Resultado));
                return View(_mapper.Map<HotelUpdateDto>(model));
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActualizarHotel(HotelUpdateDto modelo)
        {
            if (ModelState.IsValid)
            {
                var respose = await _hotelService.Actualizar<APIResponse>(modelo, HttpContext.Session.GetString(DS.SessionToken));

                if(respose != null && respose.IsExitoso)
                {
					TempData["exitoso"] = "Hotel Actualizado Exitosamente";
					return RedirectToAction(nameof(IndexHotel));
                }
            }
            return View(modelo);
        }


        public async Task<IActionResult> RemoverHotel(int hotelId)
        {
            var response = await _hotelService.Obtener<APIResponse>(hotelId, HttpContext.Session.GetString(DS.SessionToken));

            if (response != null && response.IsExitoso)
            {
                HotelDto model = JsonConvert.DeserializeObject<HotelDto>(Convert.ToString(response.Resultado));
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoverHotel(HotelDto modelo)
        {
            var respose = await _hotelService.Remover<APIResponse>(modelo.Id, HttpContext.Session.GetString(DS.SessionToken));

            if (respose != null && respose.IsExitoso)
            {
				TempData["exitoso"] = "Hotel Eliminado Exitosamente";
				return RedirectToAction(nameof(IndexHotel));
            }
			TempData["error"] = "Ocurrio un Error al Remover";
			return View(modelo);
        }

    }
}
