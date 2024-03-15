using GestionUsuarios.DOMAIN.Dto;

namespace GestionUsuarios.APPLICATION.Services;

public interface IUsuarioService
{
    Task<RespuestaDto> RegisterAsync(UsuarioDto model);
    Task<DatosUsuarioDto> GetTokenAsync(UsuarioDto model);
}
