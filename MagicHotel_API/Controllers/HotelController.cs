using MagicHotel_API.Datos;
using MagicHotel_API.Modelos;
using MagicHotel_API.Modelos.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicHotel_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly ILogger<HotelController> _logger; // Registra info sobre el comportamiento de la API
        private readonly ApplicationDbContext _db;         // DbContext para usar datos de la DB

        public HotelController(ILogger<HotelController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        // Obtener Lista.
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<HotelDto>> GetHoteles()
        {
            _logger.LogInformation("Obtener los Hoteles");
            return Ok(_db.Hoteles.ToList()); // Cod de estado 200.
        }

        // Obtener solo un Hotel.
        [HttpGet("id:int", Name = "GetHotel")]
        // Documentar codigos de Estados:
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<HotelDto> GetHotel(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Error al traer el Hotel con Id " + id);
                return BadRequest(); // Cod de estado 400 Solucitud Incorrecta.
            }
            // Busca el Id que que coincida con el (id) que llega.
            //var hotel = HotelStore.hotelList.FirstOrDefault(h => h.Id == id);   Lista de HotelStore
            var hotel = _db.Hoteles.FirstOrDefault(h => h.Id == id);            //Lista de DB

            if (hotel == null)
            {
                return NotFound(); // Cod de estado 404 No encontrado.
            }

            return Ok(hotel);
        }

        // Agregar un Hotel
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<HotelDto> CrearHotel([FromBody] HotelDto hotelDto)
        {
            // Validacion del HotelDto
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            // Validar Nombres repetidos
            if (_db.Hoteles.FirstOrDefault(h => h.Nombre.ToLower() == hotelDto.Nombre.ToLower()) != null)
            {
                ModelState.AddModelError("NombreExiste", "El Hotel con ese Nombre ya existe!");
                return BadRequest(ModelState);
            }
            // Validar null
            if (hotelDto == null)
            {
                return BadRequest();
            }
            // Tiene que llegar un id=0
            if (hotelDto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            // Ordeno la lista de Mayor a Menor, obtendo el mas alto y sumo 1, para darle su Id al Registro
            //hotelDto.Id = HotelStore.hotelList.OrderByDescending(h => h.Id).FirstOrDefault().Id + 1;
            // Agrego a la lista
            //HotelStore.hotelList.Add(hotelDto);

            // Agrego los datos al Modelo y mando a DB:
            Hotel modelo = new()
            {
                Nombre = hotelDto.Nombre,
                Detalle = hotelDto.Detalle,
                ImagenUrl = hotelDto.ImagenUrl,
                Ocupantes = hotelDto.Ocupantes,
                Tarifa = hotelDto.Tarifa,
                MetrosCuadrados = hotelDto.MetrosCuadrados,
                Amenidad = hotelDto.Amenidad
            };

            _db.Hoteles.Add(modelo);
            _db.SaveChanges();

            // Retorno la ruta del nuevo registro
            return CreatedAtRoute("GetHotel", new { id = hotelDto.Id }, hotelDto);
        }

        // Eliminar un Hotel
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteHotel(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            // Busco el hotel por su Id
            var hotel = _db.Hoteles.FirstOrDefault(h => h.Id == id);
            if (hotel == null)
            {
                return NotFound();
            }
            // Si pasa las validaciones anteriores es porque encontro un Id, entonces elimina
            //HotelStore.hotelList.Remove(hotel);
            _db.Hoteles.Remove(hotel);
            _db.SaveChanges();

            return NoContent();
        }

        // Actualizar Registro Completo
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateHotel(int id, [FromBody] HotelDto hotelDto)
        {
            if (hotelDto == null || id != hotelDto.Id)
            {
                return BadRequest();
            }
            //var hotel = HotelStore.hotelList.FirstOrDefault(h => h.Id == id);
            //hotel.Nombre = hotelDto.Nombre;
            //hotel.Ocupantes = hotelDto.Ocupantes;
            //hotel.MetrosCuadrados = hotelDto.MetrosCuadrados;

            // Actualizo el Modelo
            Hotel modelo = new()
            {
                Id = hotelDto.Id,
                Nombre = hotelDto.Nombre,
                Detalle = hotelDto.Detalle,
                ImagenUrl = hotelDto.ImagenUrl,
                Ocupantes = hotelDto.Ocupantes,
                Tarifa = hotelDto.Tarifa,
                MetrosCuadrados = hotelDto.MetrosCuadrados,
                Amenidad = hotelDto.Amenidad
            };
            _db.Hoteles.Update(modelo);
            _db.SaveChanges();

            return NoContent(); 
        }

        // Actualizar solo UNA Propiedad del Registro
        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialHotel(int id, JsonPatchDocument<HotelDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }
            //var hotel = HotelStore.hotelList.FirstOrDefault(h => h.Id == id);
            var hotel = _db.Hoteles.AsNoTracking().FirstOrDefault(h => h.Id == id);
            HotelDto hotelDto = new()
            {
                Id = hotel.Id,
                Nombre = hotel.Nombre,
                Detalle= hotel.Detalle,
                ImagenUrl= hotel.ImagenUrl,
                Ocupantes= hotel.Ocupantes,
                Tarifa= hotel.Tarifa,
                MetrosCuadrados = hotel.MetrosCuadrados,
                Amenidad = hotel.Amenidad
            };

            if (hotel == null) return BadRequest();

            patchDto.ApplyTo(hotelDto, ModelState);
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Hotel modelo = new()
            {
                Id = hotelDto.Id,
                Nombre = hotelDto.Nombre,
                Detalle = hotelDto.Detalle,
                ImagenUrl = hotelDto.ImagenUrl,
                Ocupantes = hotelDto.Ocupantes,
                Tarifa = hotelDto.Tarifa,
                MetrosCuadrados = hotelDto.MetrosCuadrados,
                Amenidad = hotelDto.Amenidad
            };

            _db.Hoteles.Update(modelo);
            _db.SaveChanges();
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
