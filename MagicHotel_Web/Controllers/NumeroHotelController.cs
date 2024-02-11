using AutoMapper;
using MagicHotel_Utilidad;
using MagicHotel_Web.Models;
using MagicHotel_Web.Models.Dto;
using MagicHotel_Web.Models.ViewModel;
using MagicHotel_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace MagicHotel_Web.Controllers
{
    public class NumeroHotelController : Controller
    {
        private readonly INumeroHotelService _numeroHotelService;
        private readonly IHotelService _hotelService;
        private readonly IMapper _mapper;
        public NumeroHotelController(INumeroHotelService numeroHotelService,IHotelService hotelService, IMapper mapper)
        {
            _numeroHotelService = numeroHotelService;
            _hotelService = hotelService;
            _mapper = mapper;
        }

        public async Task<IActionResult> IndexNumeroHotel()
        {
            List<NumeroHotelDto> numeroHotelList = new();

            var response = await _numeroHotelService.ObtenerTodos<APIResponse>(HttpContext.Session.GetString(DS.SessionToken));

            if(response != null && response.IsExitoso)
            {
                numeroHotelList = JsonConvert.DeserializeObject<List<NumeroHotelDto>>(Convert.ToString(response.Resultado));
            }

            return View(numeroHotelList);
        }

        public async Task<IActionResult> CrearNumeroHotel()
        {
            NumeroHotelViewModel numeroHotelVM = new();
            var response = await _hotelService.ObtenerTodos<APIResponse>(HttpContext.Session.GetString(DS.SessionToken));
            if(response != null && response.IsExitoso)
            {
                numeroHotelVM.HotelList = JsonConvert.DeserializeObject<List<HotelDto>>(Convert.ToString(response.Resultado))
                                          .Select(h => new SelectListItem
                                          {
                                              Text = h.Nombre,
                                              Value = h.Id.ToString()
                                          });
            }

            return View(numeroHotelVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearNumeroHotel(NumeroHotelViewModel modelo)
        {
            if (ModelState.IsValid)
            {
                var response = await _numeroHotelService.Crear<APIResponse>(modelo.NumeroHotel, HttpContext.Session.GetString(DS.SessionToken));
                if (response != null && response.IsExitoso)
                {
					TempData["exitoso"] = "Numero Hotel Creado Exitosamente";
					return RedirectToAction(nameof(IndexNumeroHotel));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }

            var res = await _hotelService.ObtenerTodos<APIResponse>(HttpContext.Session.GetString(DS.SessionToken));
            if (res != null && res.IsExitoso)
            {
                modelo.HotelList = JsonConvert.DeserializeObject<List<HotelDto>>(Convert.ToString(res.Resultado))
                                          .Select(h => new SelectListItem
                                          {
                                              Text = h.Nombre,
                                              Value = h.Id.ToString()
                                          });
            }

            return View(modelo);
        }


        public async Task<IActionResult> ActualizarNumeroHotel(int hotelNo)
        {
            NumeroHotelUpdateViewModel numeroHotelVM = new();

            var response = await _numeroHotelService.Obtener<APIResponse>(hotelNo, HttpContext.Session.GetString(DS.SessionToken));
            if(response != null && response.IsExitoso)
            {
                NumeroHotelDto modelo = JsonConvert.DeserializeObject<NumeroHotelDto>(Convert.ToString(response.Resultado));
                numeroHotelVM.NumeroHotel = _mapper.Map<NumeroHoteUpdatelDto>(modelo);
            }

            response = await _hotelService.ObtenerTodos<APIResponse>(HttpContext.Session.GetString(DS.SessionToken));
            if (response != null && response.IsExitoso)
            {
                numeroHotelVM.HotelList = JsonConvert.DeserializeObject<List<HotelDto>>(Convert.ToString(response.Resultado))
                                          .Select(h => new SelectListItem
                                          {
                                              Text = h.Nombre,
                                              Value = h.Id.ToString()
                                          });
                return View(numeroHotelVM);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActualizarNumeroHotel(NumeroHotelUpdateViewModel modelo)
        {
            if (ModelState.IsValid)
            {
                var response = await _numeroHotelService.Actualizar<APIResponse>(modelo.NumeroHotel, HttpContext.Session.GetString(DS.SessionToken));
                if (response != null && response.IsExitoso)
                {
					TempData["exitoso"] = "Numero Hotel Actualizado Exitosamente";
					return RedirectToAction(nameof(IndexNumeroHotel));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }

            var res = await _hotelService.ObtenerTodos<APIResponse>(HttpContext.Session.GetString(DS.SessionToken));
            if (res != null && res.IsExitoso)
            {
                modelo.HotelList = JsonConvert.DeserializeObject<List<HotelDto>>(Convert.ToString(res.Resultado))
                                          .Select(h => new SelectListItem
                                          {
                                              Text = h.Nombre,
                                              Value = h.Id.ToString()
                                          });
            }

            return View(modelo);
        }


        public async Task<IActionResult> RemoverNumeroHotel(int hotelNo)
        {
            NumeroHotelDeleteViewModel numeroHotelVM = new();

            var response = await _numeroHotelService.Obtener<APIResponse>(hotelNo, HttpContext.Session.GetString(DS.SessionToken));
            if (response != null && response.IsExitoso)
            {
                NumeroHotelDto modelo = JsonConvert.DeserializeObject<NumeroHotelDto>(Convert.ToString(response.Resultado));
                numeroHotelVM.NumeroHotel = modelo;
            }

            response = await _hotelService.ObtenerTodos<APIResponse>(HttpContext.Session.GetString(DS.SessionToken));
            if (response != null && response.IsExitoso)
            {
                numeroHotelVM.HotelList = JsonConvert.DeserializeObject<List<HotelDto>>(Convert.ToString(response.Resultado))
                                          .Select(h => new SelectListItem
                                          {
                                              Text = h.Nombre,
                                              Value = h.Id.ToString()
                                          });
                return View(numeroHotelVM);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoverNumeroHotel(NumeroHotelDeleteViewModel modelo)
        {
            var response = await _numeroHotelService.Remover<APIResponse>(modelo.NumeroHotel.HotelNo, HttpContext.Session.GetString(DS.SessionToken));
            if(response != null && response.IsExitoso)
            {
				TempData["exitoso"] = "Numero Hotel Eliminado Exitosamente";
				return RedirectToAction(nameof(IndexNumeroHotel));
            }
			TempData["error"] = "Un Error Ocurrio al Remover";
			return View(modelo);
        }

    }
}
