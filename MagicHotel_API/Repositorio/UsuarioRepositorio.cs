using MagicHotel_API.Datos;
using MagicHotel_API.Modelos;
using MagicHotel_API.Modelos.Dto;
using MagicHotel_API.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MagicHotel_API.Repositorio
{
	public class UsuarioRepositorio : IUsuarioRepositorio
	{
		private readonly ApplicationDbContext _db;
        private string secretKey { get; set; }

        public UsuarioRepositorio(ApplicationDbContext db, IConfiguration configuration)
        {
			_db = db;
			secretKey = configuration.GetValue<string>("ApiSettings:Secret");
        }

        public bool IsUsuarioUnico(string userName)
		{
			var usuario = _db.Usuarios.FirstOrDefault(u => u.UserName.ToLower() == userName.ToLower());
			if(usuario == null)
			{
				return true;
			}
			return false;
		}

		public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
		{
			var usuario = await _db.Usuarios.FirstOrDefaultAsync(u=> u.UserName.ToLower() == loginRequestDTO.UserName.ToLower() && 
															         u.Password == loginRequestDTO.Password);
			if(usuario == null)
			{
				return new LoginResponseDTO()
				{
					Token = "",
					Usuario = null
				};
			}
			// Si Usuario Existe Generamos el JW Token
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(secretKey);
			var tokenDescription = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(ClaimTypes.Name, usuario.Id.ToString()),
					new Claim(ClaimTypes.Role, usuario.Rol)
				}),
				Expires = DateTime.UtcNow.AddDays(7),
				SigningCredentials = new (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
			};
			var token = tokenHandler.CreateToken(tokenDescription);
			LoginResponseDTO loginResponseDTO = new()
			{
				Token = tokenHandler.WriteToken(token),
				Usuario = usuario,
			};
			return loginResponseDTO;
		}

		public async Task<Usuario> Registrar(RegistroRequestDTO registroRequestDTO)
		{
			Usuario usuario = new()
			{
				UserName = registroRequestDTO.UserName,
				Password = registroRequestDTO.Password,
				Nombres = registroRequestDTO.Nombres,
				Rol = registroRequestDTO.Rol,
			};
			await _db.Usuarios.AddAsync(usuario);
			await _db.SaveChangesAsync();
			usuario.Password = "";
			return usuario;
		}
	}
}
