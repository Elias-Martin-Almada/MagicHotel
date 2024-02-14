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

namespace MagicHotel_API.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
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

        // Obtener Lista.
        [HttpGet]
        [Authorize(Roles = "admin", AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetNumeroHoteles()
        {
            try
            {
                _logger.LogInformation("Obtener Numeros de Hoteles");

                IEnumerable<NumeroHotel> NumeroHotelList = await _numeroRepo.ObtenerTodos(incluirPropiedades: "Hotel");

                _response.Resultado = _mapper.Map<IEnumerable<NumeroHotelDto>>(NumeroHotelList);
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


        // Obtener solo un Hotel.
        [HttpGet("{id:int}", Name = "GetNumeroHotel")]
        [Authorize(Roles = "admin", AuthenticationSchemes = "Bearer")]
        // Documentar codigos de Estados:
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetNumeroHotel(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al traer Numero de Hotel con Id " + id);
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.IsExitoso = false;
                    return BadRequest(_response); // Cod de estado 400 Solucitud Incorrecta.
                }
                // Busca el Id que que coincida con el (id) que llega.
                //var hotel = HotelStore.hotelList.FirstOrDefault(h => h.Id == id);   Lista de HotelStore
                var numeroHotel = await _numeroRepo.Obtener(h => h.HotelNo == id, incluirPropiedades: "Hotel");            //Lista de DB

                if (numeroHotel == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.IsExitoso = false;
                    return NotFound(_response); // Cod de estado 404 No encontrado.
                }

                _response.Resultado = _mapper.Map<NumeroHotelDto>(numeroHotel);
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
        [Authorize(Roles = "admin", AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CrearNumeroHotel([FromBody] NumeroHotelCreateDto createDto)
        {
            try
            {
                // Validacion del HotelDto
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                // Validar Nombres repetidos
                if (await _numeroRepo.Obtener(h => h.HotelNo == createDto.HotelNo) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "El numero de Hotel ya existe!");
                    return BadRequest(ModelState);
                }

                if (await _hotelRepo.Obtener(h => h.Id == createDto.HotelId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "El Id del Hotel no existe!");
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
                NumeroHotel modelo = _mapper.Map<NumeroHotel>(createDto);

                modelo.FechaCreacion = DateTime.Now;
                modelo.FechaActualizacion = DateTime.Now;
                await _numeroRepo.Crear(modelo);
                _response.Resultado = modelo;
                _response.statusCode = HttpStatusCode.Created;

                // Retorno la ruta del nuevo registro
                return CreatedAtRoute("GetNumeroHotel", new { id = modelo.HotelNo }, _response);
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
        [Authorize(Roles = "admin", AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteNumeroHotel(int id)
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
                var numeroHotel = await _numeroRepo.Obtener(h => h.HotelNo == id);
                if (numeroHotel == null)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.NoContent;
                    return NotFound(_response);
                }
                // Si pasa las validaciones anteriores es porque encontro un Id, entonces elimina
                //HotelStore.hotelList.Remove(hotel);
                await _numeroRepo.Remover(numeroHotel);

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
        [Authorize(Roles = "admin", AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateNumeroHotel(int id, [FromBody] NumeroHoteUpdatelDto updateDto)
        {
            if (updateDto == null || id != updateDto.HotelNo)
            {
                _response.IsExitoso = false;
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            if (await _hotelRepo.Obtener(h => h.Id == updateDto.HotelId) == null)
            {
                ModelState.AddModelError("ErrorMessages", "El Id del Hotel No existe!");
                return BadRequest(ModelState);
            }

            // Actualizo el Modelo
            NumeroHotel modelo = _mapper.Map<NumeroHotel>(updateDto);

            await _numeroRepo.Actualizar(modelo); // Update no es un metodo Async
            _response.statusCode = HttpStatusCode.NoContent;

            return Ok(_response);
        }

        // Actualizar solo UNA Propiedad del Registro

    }

}
