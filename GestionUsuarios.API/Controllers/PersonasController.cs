using AutoMapper;
using GestionUsuarios.APPLICATION.Common.Interfaces;
using GestionUsuarios.DOMAIN.Dto;
using GestionUsuarios.DOMAIN.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GestionUsuarios.API.Controllers;

public class PersonasController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public PersonasController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    /// <summary>
    /// Obtiene todos las personas.
    /// </summary>
    /// <returns>Una respuesta HTTP con el resultado de la operación.</returns>
    [HttpGet]
    public async Task<ActionResult<RespuestaDto>> Get()
    {
        var respuesta = new RespuestaDto();

        try
        {
            var datos = await _unitOfWork.PersonaRepository.GetAll();

            respuesta.Estado = "Éxito";
            respuesta.Mensaje = "Datos obtenidos correctamente";
            respuesta.Ok = true;
            respuesta.Datos = datos;

            return Ok(respuesta);
        }
        catch (Exception ex)
        {
            respuesta.Estado = "Error";
            respuesta.Mensaje = "Ha ocurrido un error al obtener los datos" + ex.Message;
            respuesta.Ok = false;
            respuesta.Datos = null;
            return StatusCode(500, respuesta);
        }
    }

    /// <summary>
    /// Obtiene una persona por su ID.
    /// </summary>
    /// <param name="id">El ID de la persona a obtener.</param>
    /// <returns>Una respuesta HTTP con el resultado de la operación.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<RespuestaDto>> GetById(int id)
    {
        var respuesta = new RespuestaDto();

        try
        {
            var persona = await _unitOfWork.PersonaRepository.GetById(id);

            if (persona == null)
            {
                respuesta.Estado = "Error";
                respuesta.Mensaje = $"Persona con ID {id} no encontrado";
                respuesta.Ok = false;
                respuesta.Datos = null;
                return NotFound(respuesta);
            }

            respuesta.Estado = "Éxito";
            respuesta.Mensaje = "Persona encontrado correctamente";
            respuesta.Ok = true;
            respuesta.Datos = persona;

            return Ok(respuesta);
        }
        catch (Exception ex)
        {
            respuesta.Estado = "Error";
            respuesta.Mensaje = $"Ha ocurrido un error al obtener la persona con ID {id}: {ex.Message}";
            respuesta.Ok = false;
            respuesta.Datos = null;
            return StatusCode(500, respuesta);
        }
    }


    /// <summary>
    /// Crea una nueva persona.
    /// </summary>
    /// <param name="personaDto">La persona a crear.</param>
    /// <returns>Una respuesta HTTP con el resultado de la operación.</returns>
    [HttpPost]
    public async Task<ActionResult<RespuestaDto>> Create([FromBody] PersonaDto personaDto)
    {
        var respuesta = new RespuestaDto();

        try
        {

            var persona = _mapper.Map<Persona>(personaDto);
            await _unitOfWork.PersonaRepository.Add(persona);
            await _unitOfWork.SaveChangesAsync();

            respuesta.Estado = "Éxito";
            respuesta.Mensaje = "Persona creado correctamente";
            respuesta.Ok = true;
            respuesta.Datos = personaDto;

            return Ok(respuesta);
        }
        catch (Exception ex)
        {
            respuesta.Estado = "Error";
            respuesta.Mensaje = "Ha ocurrido un error al crear la persona : " + ex.Message;
            respuesta.Ok = false;
            respuesta.Datos = null;
            return StatusCode(500, respuesta);
        }
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
        var respuesta = new RespuestaDto();

        try
        {
            var personaExistente = await _unitOfWork.PersonaRepository.GetById(id);

            if (personaExistente == null)
            {
                respuesta.Estado = "Error";
                respuesta.Mensaje = $"persona con ID {id} no encontrado";
                respuesta.Ok = false;
                respuesta.Datos = null;
                return NotFound(respuesta);
            }

            personaExistente.Nombres = personaDto.Nombres;
            personaExistente.Apellidos = personaDto.Apellidos;
            personaExistente.NumeroIdentificacion = personaDto.NumeroIdentificacion;

            personaExistente.Email = personaDto.Email;
            personaExistente.TipoIdentificacion = personaDto.TipoIdentificacion;
            personaExistente.FechaCreacion = DateTime.Now;

            _unitOfWork.PersonaRepository.Update(personaExistente);
            await _unitOfWork.SaveChangesAsync();

            respuesta.Estado = "Éxito";
            respuesta.Mensaje = $"persona con ID {id} actualizado correctamente";
            respuesta.Ok = true;
            respuesta.Datos = personaExistente;

            return Ok(respuesta);
        }
        catch (Exception ex)
        {
            respuesta.Estado = "Error";
            respuesta.Mensaje = $"Ha ocurrido un error al actualizar la persona con ID {id}: {ex.Message}";
            respuesta.Ok = false;
            respuesta.Datos = null;
            return StatusCode(500, respuesta);
        }
    }

    /// <summary>
    /// Elimina una persona existente.
    /// </summary>
    /// <param name="id">El ID de la persona a eliminar.</param>
    /// <returns>Una respuesta HTTP con el resultado de la operación.</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<RespuestaDto>> Delete(int id)
    {
        var respuesta = new RespuestaDto();

        try
        {
            var usuarioExistente = await _unitOfWork.PersonaRepository.GetById(id);

            if (usuarioExistente == null)
            {
                respuesta.Estado = "Error";
                respuesta.Mensaje = $"persona con ID {id} no encontrado";
                respuesta.Ok = false;
                respuesta.Datos = null;
                return NotFound(respuesta);
            }

            _unitOfWork.PersonaRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();

            respuesta.Estado = "Éxito";
            respuesta.Mensaje = $"persona con ID {id} eliminado correctamente";
            respuesta.Ok = true;
            respuesta.Datos = null;

            return Ok(respuesta);
        }
        catch (Exception ex)
        {
            respuesta.Estado = "Error";
            respuesta.Mensaje = $"Ha ocurrido un error al eliminar la persona con ID {id}: {ex.Message}";
            respuesta.Ok = false;
            respuesta.Datos = null;
            return StatusCode(500, respuesta);
        }
    }
}
