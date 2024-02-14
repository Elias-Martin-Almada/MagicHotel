using Microsoft.AspNetCore.Identity;

namespace MagicHotel_API.Modelos
{
    public class UsuarioAplicacion : IdentityUser
    {
        public string Nombres { get; set; }
    }
}
