using AutoMapper;
using MagicHotel_API.Datos;
using MagicHotel_API.Modelos;
using MagicHotel_API.Modelos.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace MagicHotel_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly ILogger<HotelController> _logger; // Registra info sobre el comportamiento de la API
        private readonly ApplicationDbContext _db;         // DbContext para usar datos de la DB
        private readonly IMapper _mapper;

        public HotelController(ILogger<HotelController> logger, ApplicationDbContext db, IMapper mapper)
        {
            _logger = logger;
            _db = db;
            _mapper = mapper;
        }

        // Obtener Lista.
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<HotelDto>>> GetHoteles()
        {
            _logger.LogInformation("Obtener los Hoteles");

            IEnumerable<Hotel> hotelList = await _db.Hoteles.ToListAsync();

            return Ok(_mapper.Map<IEnumerable<HotelDto>>(hotelList)); // Cod de estado 200.
        }

        // Obtener solo un Hotel.
        [HttpGet("id:int", Name = "GetHotel")]
        // Documentar codigos de Estados:
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<HotelDto>> GetHotel(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Error al traer el Hotel con Id " + id);
                return BadRequest(); // Cod de estado 400 Solucitud Incorrecta.
            }
            // Busca el Id que que coincida con el (id) que llega.
            //var hotel = HotelStore.hotelList.FirstOrDefault(h => h.Id == id);   Lista de HotelStore
            var hotel = await _db.Hoteles.FirstOrDefaultAsync(h => h.Id == id);            //Lista de DB

            if (hotel == null)
            {
                return NotFound(); // Cod de estado 404 No encontrado.
            }

            return Ok(_mapper.Map<HotelDto>(hotel));
        }

        // Agregar un Hotel
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HotelDto>> CrearHotel([FromBody] HotelCreateDto createDto)
        {
            // Validacion del HotelDto
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            // Validar Nombres repetidos
            if (await _db.Hoteles.FirstOrDefaultAsync(h => h.Nombre.ToLower() == createDto.Nombre.ToLower()) != null)
            {
                ModelState.AddModelError("NombreExiste", "El Hotel con ese Nombre ya existe!");
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

            await _db.Hoteles.AddAsync(modelo);
            await _db.SaveChangesAsync();

            // Retorno la ruta del nuevo registro
            return CreatedAtRoute("GetHotel", new { id = modelo.Id }, modelo);
        }

        // Eliminar un Hotel
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            // Busco el hotel por su Id
            var hotel = await _db.Hoteles.FirstOrDefaultAsync(h => h.Id == id);
            if (hotel == null)
            {
                return NotFound();
            }
            // Si pasa las validaciones anteriores es porque encontro un Id, entonces elimina
            //HotelStore.hotelList.Remove(hotel);
            _db.Hoteles.Remove(hotel);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        // Actualizar Registro Completo
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateHotel(int id, [FromBody] HotelUpdateDto updateDto)
        {
            if (updateDto == null || id != updateDto.Id)
            {
                return BadRequest();
            }


            // Actualizo el Modelo
            Hotel modelo = _mapper.Map<Hotel>(updateDto);

            _db.Hoteles.Update(modelo); // Update no es un metodo Async
            await _db.SaveChangesAsync();
            return NoContent(); 
        }

        // Actualizar solo UNA Propiedad del Registro
        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialHotel(int id, JsonPatchDocument<HotelUpdateDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }
            //var hotel = HotelStore.hotelList.FirstOrDefault(h => h.Id == id);
            var hotel = await _db.Hoteles.AsNoTracking().FirstOrDefaultAsync(h => h.Id == id);

            HotelUpdateDto hotelDto = _mapper.Map<HotelUpdateDto>(hotel);

            if (hotel == null) return BadRequest();

            patchDto.ApplyTo(hotelDto, ModelState);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Hotel modelo = _mapper.Map<Hotel>(hotelDto);

            _db.Hoteles.Update(modelo);
            await _db.SaveChangesAsync();
            return NoContent();
        } 
        // Para usar el PATCH:
        //  "operationType": 0,
        //  "path": "/metroscuadrados", <=Nombre de Propiedad
        //  "op": "replace",            <=Operacion
        //  "from": "string",
        //  "value": "35"               <=Dato Nuevo

    }

}
