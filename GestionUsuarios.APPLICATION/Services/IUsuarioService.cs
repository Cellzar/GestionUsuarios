using GestionUsuarios.DOMAIN.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GestionUsuarios.APPLICATION.Services;

public interface IUsuarioService
{
    Task<RespuestaDto> RegisterAsync(UsuarioDto model);
    Task<DatosUsuarioDto> GetTokenAsync(UsuarioDto model);
    Task<RespuestaDto> Get();
    Task<RespuestaDto> GetById(int id);
    Task<RespuestaDto> Update(int id, [FromBody] UsuarioDto usuarioDto);
    Task<RespuestaDto> Delete(int id);
}
