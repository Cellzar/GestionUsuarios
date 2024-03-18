using AutoMapper;
using GestionUsuarios.APPLICATION.Common.Interfaces;
using GestionUsuarios.APPLICATION.Services;
using GestionUsuarios.DOMAIN.Dto;
using GestionUsuarios.DOMAIN.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestionUsuarios.API.Controllers;

[Authorize]
public class PersonasController : BaseApiController
{
    private readonly PersonaService _personaService;
    private readonly IMapper _mapper;
    public PersonasController(PersonaService personaService, IMapper mapper)
    {
        _personaService = personaService;
        _mapper = mapper;
    }

    /// <summary>
    /// Obtiene todos las personas.
    /// </summary>
    /// <returns>Una respuesta HTTP con el resultado de la operación.</returns>
    [HttpGet]
    public async Task<ActionResult<RespuestaDto>> Get()
    {
        var result = await _personaService.Get();
        return Ok(result);
    }

    /// <summary>
    /// Obtiene una persona por su ID.
    /// </summary>
    /// <param name="id">El ID de la persona a obtener.</param>
    /// <returns>Una respuesta HTTP con el resultado de la operación.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<RespuestaDto>> GetById(int id)
    {
        var result = await _personaService.GetById(id);
        return Ok(result);
    }


    /// <summary>
    /// Crea una nueva persona.
    /// </summary>
    /// <param name="personaDto">La persona a crear.</param>
    /// <returns>Una respuesta HTTP con el resultado de la operación.</returns>
    [HttpPost]
    public async Task<ActionResult<RespuestaDto>> Create([FromBody] PersonaDto personaDto)
    {
        var persona = _mapper.Map<Persona>(personaDto);
        var result = await _personaService.Create(persona);
        return Ok(result);
    }

    /// <summary>
    /// Actualiza una persona existente.
    /// </summary>
    /// <param name="id">El ID de la persona a actualizar.</param>
    /// <param name="persona">El objeto Usuario con los datos actualizados.</param>
    /// <returns>Una respuesta HTTP con el resultado de la operación.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<RespuestaDto>> Update(int id, [FromBody] PersonaDto personaDto)
    {
        var result = await _personaService.Update(id, personaDto);
        return Ok(result);
    }

    /// <summary>
    /// Elimina una persona existente.
    /// </summary>
    /// <param name="id">El ID de la persona a eliminar.</param>
    /// <returns>Una respuesta HTTP con el resultado de la operación.</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<RespuestaDto>> Delete(int id)
    {
        var result = await _personaService.Delete(id);
        return Ok(result);
    }
}
