using GestionUsuarios.DOMAIN.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GestionUsuarios.APPLICATION.Services;

public interface IUsuarioService
{
    Task<RespuestaDto> RegisterAsync(UsuarioDto model);
    Task<DatosUsuarioDto> GetTokenAsync(UsuarioDto model);
    Task<ActionResult<RespuestaDto>> Get();
    Task<ActionResult<RespuestaDto>> GetById(int id);
    Task<ActionResult<RespuestaDto>> Update(int id, [FromBody] UsuarioDto usuarioDto);
    Task<ActionResult<RespuestaDto>> Delete(int id);
}
