using AutoMapper;
using MagicHotel_API.Datos;
using MagicHotel_API.Modelos;
using MagicHotel_API.Modelos.Dto;
using MagicHotel_API.Modelos.Especificaciones;
using MagicHotel_API.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Numerics;

namespace MagicHotel_API.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class HotelController : ControllerBase
    {
        private readonly ILogger<HotelController> _logger; // Registra info sobre el comportamiento de la API
        private readonly IHotelRepositorio _hotelRepo;         // DbContext para usar datos de la DB
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public HotelController(ILogger<HotelController> logger, IHotelRepositorio hotelRepositorio, IMapper mapper)
        {
            _logger = logger;
            _hotelRepo = hotelRepositorio;
            _mapper = mapper;
            _response = new();
        }

        // Obtener Lista.
        [HttpGet]
        [ResponseCache(CacheProfileName = "Default30")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetHoteles()
        {
            try
            {
                _logger.LogInformation("Obtener los Hoteles");

                IEnumerable<Hotel> hotelList = await _hotelRepo.ObtenerTodos();

                _response.Resultado = _mapper.Map<IEnumerable<HotelDto>>(hotelList);
                _response.statusCode = HttpStatusCode.OK;

                return Ok(_response); // Cod de estado 200.
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("HotelesPaginado")] // Esto lo ocupo en HotelService ObtenerTodosPaginado<T> WEB
        [ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<APIResponse> GetHotelesPaginados([FromQuery] Parametros parametros)
        {
            try
            {
                var hotelList = _hotelRepo.ObtenerTodosPaginado(parametros);
                _response.Resultado = _mapper.Map<IEnumerable<HotelDto>>(hotelList);
                _response.statusCode = HttpStatusCode.OK;
                _response.TotalPaginas = hotelList.MetaData.TotalPages;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        // Obtener solo un Hotel.
        [HttpGet("{id:int}", Name = "GetHotel")]
        [Authorize]
        // Documentar codigos de Estados:
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetHotel(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al traer el Hotel con Id " + id);
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.IsExitoso = false;
                    return BadRequest(_response); // Cod de estado 400 Solucitud Incorrecta.
                }
                // Busca el Id que que coincida con el (id) que llega.
                //var hotel = HotelStore.hotelList.FirstOrDefault(h => h.Id == id);   Lista de HotelStore
                var hotel = await _hotelRepo.Obtener(h => h.Id == id);            //Lista de DB

                if (hotel == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.IsExitoso = false;
                    return NotFound(_response); // Cod de estado 404 No encontrado.
                }

                _response.Resultado = _mapper.Map<HotelDto>(hotel);
                _response.statusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        // Agregar un Hotel
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CrearHotel([FromBody] HotelCreateDto createDto)
        {
            try
            {
                // Validacion del HotelDto
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                // Validar Nombres repetidos
                if (await _hotelRepo.Obtener(h => h.Nombre.ToLower() == createDto.Nombre.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "El Hotel con ese Nombre ya existe!");
                    return BadRequest(ModelState);
                }
                // Validar null
                if (createDto == null)
                {
                    return BadRequest(createDto);
                }

                // Ordeno la lista de Mayor a Menor, obtendo el mas alto y sumo 1, para darle su Id al Registro
                //hotelDto.Id = HotelStore.hotelList.OrderByDescending(h => h.Id).FirstOrDefault().Id + 1;
                // Agrego a la lista
                //HotelStore.hotelList.Add(hotelDto);

                // Agrego los datos al Modelo y mando a DB:
                Hotel modelo = _mapper.Map<Hotel>(createDto);

                modelo.FechaCreacion = DateTime.Now;
                modelo.FechaActualizacion = DateTime.Now;
                await _hotelRepo.Crear(modelo);
                _response.Resultado = modelo;
                _response.statusCode = HttpStatusCode.Created;

                // Retorno la ruta del nuevo registro
                return CreatedAtRoute("GetHotel", new { id = modelo.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        // Eliminar un Hotel
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                // Busco el hotel por su Id
                var hotel = await _hotelRepo.Obtener(h => h.Id == id);
                if (hotel == null)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.NoContent;
                    return NotFound(_response);
                }
                // Si pasa las validaciones anteriores es porque encontro un Id, entonces elimina
                //HotelStore.hotelList.Remove(hotel);
                await _hotelRepo.Remover(hotel);

                _response.statusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return BadRequest(_response);
        }

        // Actualizar Registro Completo
        [HttpPut("{id:int}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateHotel(int id, [FromBody] HotelUpdateDto updateDto)
        {
            if (updateDto == null || id != updateDto.Id)
            {
                _response.IsExitoso = false;
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            // Actualizo el Modelo
            Hotel modelo = _mapper.Map<Hotel>(updateDto);

            await _hotelRepo.Actualizar(modelo); // Update no es un metodo Async
            _response.statusCode = HttpStatusCode.NoContent;

            return Ok(_response);
        }

        // Actualizar solo UNA Propiedad del Registro
        [HttpPatch("{id:int}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialHotel(int id, JsonPatchDocument<HotelUpdateDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }
            //var hotel = HotelStore.hotelList.FirstOrDefault(h => h.Id == id);
            var hotel = await _hotelRepo.Obtener(h => h.Id == id, tracked: false);

            HotelUpdateDto hotelDto = _mapper.Map<HotelUpdateDto>(hotel);

            if (hotel == null) return BadRequest();

            patchDto.ApplyTo(hotelDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Hotel modelo = _mapper.Map<Hotel>(hotelDto);

            await _hotelRepo.Actualizar(modelo);
            _response.statusCode = HttpStatusCode.NoContent;

            return Ok(_response);
        }
        // Para usar el PATCH:
        //  "operationType": 0,
        //  "path": "/metroscuadrados", <=Nombre de Propiedad
        //  "op": "replace",            <=Operacion
        //  "from": "string",
        //  "value": "35"               <=Dato Nuevo

    }

}
