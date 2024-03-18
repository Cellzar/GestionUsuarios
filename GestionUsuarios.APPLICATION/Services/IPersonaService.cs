using GestionUsuarios.DOMAIN.Dto;
using GestionUsuarios.DOMAIN.Entities;
namespace GestionUsuarios.APPLICATION.Services;

public interface IPersonaService
{
    Task<RespuestaDto> Get();
    Task<RespuestaDto> GetById(int id);
    Task<RespuestaDto> Create(Persona personaDto);
    Task<RespuestaDto> Update(int id, PersonaDto personaDto);
    Task<RespuestaDto> Delete(int id);
}
