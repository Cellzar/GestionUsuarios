using GestionUsuarios.APPLICATION.Common.Interfaces;
using GestionUsuarios.DOMAIN.Dto;
using GestionUsuarios.DOMAIN.Entities;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace GestionUsuarios.APPLICATION.Services;

public class PersonaService : IPersonaService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;

    public PersonaService(IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }
    public async Task<RespuestaDto> Create(Persona personaDto)
    {
        var respuesta = new RespuestaDto();

        try
        {
            var personaExistente = _unitOfWork.PersonaRepository.Find(c =>
                                                                        c.NumeroIdentificacion == personaDto.NumeroIdentificacion
                                                                        && c.TipoIdentificacion.ToLower() == personaDto.TipoIdentificacion.ToLower())
                                                                        .SingleOrDefault();


            if (personaExistente == null)
            {
                await _unitOfWork.PersonaRepository.Add(personaDto);
                await _unitOfWork.SaveChangesAsync();

                respuesta.Estado = "Éxito";
                respuesta.Mensaje = "Persona creado correctamente";
                respuesta.Ok = true;
                respuesta.Datos = personaDto;
            }
            else
            {
                respuesta.Estado = "Éxito";
                respuesta.Mensaje = "La persona ya esta registrada con esos documentos";
                respuesta.Ok = true;
            }
            
        }
        catch (Exception ex)
        {
            respuesta.Estado = "Error";
            respuesta.Mensaje = "Ha ocurrido un error al crear la persona : " + ex.Message;
            respuesta.Ok = false;
        }

        return respuesta;
    }

    public async Task<RespuestaDto> Delete(int id)
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
            }

            _unitOfWork.PersonaRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();

            respuesta.Estado = "Éxito";
            respuesta.Mensaje = $"persona con ID {id} eliminado correctamente";
            respuesta.Ok = true;
        }
        catch (Exception ex)
        {
            respuesta.Estado = "Error";
            respuesta.Mensaje = $"Ha ocurrido un error al eliminar la persona con ID {id}: {ex.Message}";
            respuesta.Ok = false;
        }

        return respuesta;
    }

    public async Task<RespuestaDto> Get()
    {
        var respuesta = new RespuestaDto();

        try
        {
            List<Persona> personas = new List<Persona>();
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand command = new SqlCommand("ConsultarPersonas", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Persona persona = new Persona
                            {
                                Identificador = Convert.ToInt32(reader["Identificador"]),
                                Nombres = Convert.ToString(reader["Nombres"]),
                                Apellidos = Convert.ToString(reader["Apellidos"]),
                                NumeroIdentificacion = Convert.ToString(reader["NumeroIdentificacion"]),
                                TipoIdentificacion = Convert.ToString(reader["TipoIdentificacion"]),
                                Email = Convert.ToString(reader["Email"]),
                                FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]),
                                NumeroIdentificacionConcatenado = Convert.ToString(reader["NumeroIdentificacionConcatenado"]),
                                NombresApellidosConcatenados = Convert.ToString(reader["NombresApellidosConcatenados"]),
                            };

                            personas.Add(persona);
                        }
                    }
                }
            }

            var datos = personas;

            respuesta.Estado = "Éxito";
            respuesta.Mensaje = "Datos obtenidos correctamente";
            respuesta.Ok = true;
            respuesta.Datos = datos;
        }
        catch (Exception ex)
        {
            respuesta.Estado = "Error";
            respuesta.Mensaje = "Ha ocurrido un error al obtener los datos" + ex.Message;
            respuesta.Ok = false;
            respuesta.Datos = null;
            
        }
        return respuesta;
    }

    public async Task<RespuestaDto> GetById(int id)
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
            }

            respuesta.Estado = "Éxito";
            respuesta.Mensaje = "Persona encontrado correctamente";
            respuesta.Ok = true;
            respuesta.Datos = persona;
        }
        catch (Exception ex)
        {
            respuesta.Estado = "Error";
            respuesta.Mensaje = $"Ha ocurrido un error al obtener la persona con ID {id}: {ex.Message}";
            respuesta.Ok = false;
            respuesta.Datos = null;
        }

        return respuesta;
    }

    public async Task<RespuestaDto> Update(int id, PersonaDto personaDto)
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
            }

            var personaExistentePorIden = _unitOfWork.PersonaRepository.Find(c =>
                                                                        c.NumeroIdentificacion == personaDto.NumeroIdentificacion
                                                                        && c.TipoIdentificacion.ToLower() == personaDto.TipoIdentificacion.ToLower())
                                                                        .SingleOrDefault();

            if (personaExistente != null)
            {
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
            }
            else
            {
                respuesta.Estado = "Éxito";
                respuesta.Mensaje = "La persona ya esta registrada con esos documentos";
                respuesta.Ok = true;
            }
        }
        catch (Exception ex)
        {
            respuesta.Estado = "Error";
            respuesta.Mensaje = $"Ha ocurrido un error al actualizar la persona con ID {id}: {ex.Message}";
            respuesta.Ok = false;
        }

        return respuesta;
    }
}
