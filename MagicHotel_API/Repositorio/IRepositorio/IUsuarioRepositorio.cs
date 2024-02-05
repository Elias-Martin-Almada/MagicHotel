using MagicHotel_API.Modelos;
using MagicHotel_API.Modelos.Dto;

namespace MagicHotel_API.Repositorio.IRepositorio
{
	public interface IUsuarioRepositorio
	{
		bool IsUsuarioUnico(string userName);

		Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);

		Task<Usuario> Registrar(RegistroRequestDTO registroRequestDTO);
	}
}
