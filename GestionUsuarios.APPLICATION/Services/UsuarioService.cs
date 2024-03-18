using GestionUsuarios.APPLICATION.Common.Interfaces;
using GestionUsuarios.DOMAIN.Dto;
using GestionUsuarios.DOMAIN.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace GestionUsuarios.APPLICATION.Services;

public class UsuarioService : IUsuarioService
{
    private readonly JWT _jwt;
    private readonly IUnitOfWork _unitOfWork;
    public UsuarioService(IUnitOfWork unitOfWork, IOptions<JWT> jwt)
    {
        _jwt = jwt.Value;
        _unitOfWork = unitOfWork;
    }

    public async Task<DatosUsuarioDto> GetTokenAsync(UsuarioDto usuarioDto)
    {
        DatosUsuarioDto datosUsuarioDto = new DatosUsuarioDto();
        var usuario = _unitOfWork.UsuarioRepository
                                    .Find(u => u.UsuarioNombre.ToLower() == usuarioDto.UsuarioNombre.ToLower())
                                    .FirstOrDefault();
        if (usuario == null)
        {
            datosUsuarioDto.Mensaje = $"No existe ningún usuario con el username {usuarioDto.UsuarioNombre}.";
            return datosUsuarioDto;
        }

        PasswordService passwordService = new PasswordService();

        var resultado = passwordService.EncodePasswordToBase64(usuarioDto.Pass);

        if (resultado == usuario.Pass)
        {
            datosUsuarioDto.Token = GenerateToken(usuario.UsuarioNombre);

            datosUsuarioDto.Mensaje = "Ingresado Correctamente";
            return datosUsuarioDto;
        }
        datosUsuarioDto.Mensaje = $"Credenciales incorrectas para el usuario {usuario.UsuarioNombre}.";
        return datosUsuarioDto;
    }

    public async Task<RespuestaDto> RegisterAsync(UsuarioDto usuarioDto)
    {
        RespuestaDto respuesta = new RespuestaDto();
        var usuario = new Usuario
        {
            UsuarioNombre = usuarioDto.UsuarioNombre,
            FechaCreacion = DateTime.Now
        };

        PasswordService passwordService = new PasswordService();

        usuario.Pass = passwordService.EncodePasswordToBase64(usuarioDto.Pass);

        var usuarioExiste = _unitOfWork.UsuarioRepository
                                    .Find(u => u.UsuarioNombre.ToLower() == usuarioDto.UsuarioNombre.ToLower())
                                    .FirstOrDefault();

        if (usuarioExiste == null)
        {
            _unitOfWork.UsuarioRepository.Add(usuario);
            await _unitOfWork.SaveChangesAsync();

            respuesta.Estado = "Éxito";
            respuesta.Mensaje = $"El usuario  {usuarioDto.UsuarioNombre} ha sido registrado exitosamente";
            respuesta.Ok = true;
            respuesta.Datos = usuario;

            return respuesta;
        }
        else
        {
            respuesta.Estado = "Éxito";
            respuesta.Mensaje = $"El usuario con {usuarioDto.UsuarioNombre} ya se encuentra registrado.";
            respuesta.Ok = false;

            return respuesta;
        }
    }

    public string GenerateToken(string username)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwt.Key);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, username)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public async Task<RespuestaDto> Get()
    {
        var respuesta = new RespuestaDto();

        try
        {
            var datos = await _unitOfWork.UsuarioRepository.GetAll();

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
        }

        return respuesta;
    }

    public async Task<RespuestaDto> GetById(int id)
    {
        var respuesta = new RespuestaDto();

        try
        {
            var persona = await _unitOfWork.UsuarioRepository.GetById(id);

            if (persona == null)
            {
                respuesta.Estado = "Error";
                respuesta.Mensaje = $"Usuario con ID {id} no encontrado";
                respuesta.Ok = false;
            }

            respuesta.Estado = "Éxito";
            respuesta.Mensaje = "Usuario encontrado correctamente";
            respuesta.Ok = true;
            respuesta.Datos = persona;

        }
        catch (Exception ex)
        {
            respuesta.Estado = "Error";
            respuesta.Mensaje = $"Ha ocurrido un error al obtener la usuario con ID {id}: {ex.Message}";
            respuesta.Ok = false;
        }
        return respuesta;
    }

    public async Task<RespuestaDto> Update(int id, [FromBody] UsuarioDto usuarioDto)
    {
        var respuesta = new RespuestaDto();

        try
        {
            var usuarioExistente = await _unitOfWork.UsuarioRepository.GetById(id);

            if (usuarioExistente == null)
            {
                respuesta.Estado = "Error";
                respuesta.Mensaje = $"Usuario con ID {id} no encontrado";
                respuesta.Ok = false;
            }

            var usuarioExiste = _unitOfWork.UsuarioRepository
                                    .Find(u => u.UsuarioNombre.ToLower() == usuarioDto.UsuarioNombre.ToLower())
                                    .FirstOrDefault();

            if (usuarioExiste != null)
            {
                PasswordService passwordService = new PasswordService();
                usuarioExistente.UsuarioNombre = usuarioDto.UsuarioNombre;
                usuarioExistente.Pass = passwordService.EncodePasswordToBase64(usuarioDto.Pass);

                _unitOfWork.UsuarioRepository.Update(usuarioExistente);
                await _unitOfWork.SaveChangesAsync();

                respuesta.Estado = "Éxito";
                respuesta.Mensaje = $"usuario con ID {id} actualizado correctamente";
                respuesta.Ok = true;
                respuesta.Datos = usuarioExistente;
            }
            else
            {
                respuesta.Estado = "Éxito";
                respuesta.Mensaje = $"El usuario con {usuarioDto.UsuarioNombre} ya se encuentra registrado.";
                respuesta.Ok = false;

                return respuesta;
            }

            

        }
        catch (Exception ex)
        {
            respuesta.Estado = "Error";
            respuesta.Mensaje = $"Ha ocurrido un error al actualizar al usuario con ID {id}: {ex.Message}";
            respuesta.Ok = false;
        }

        return respuesta;
    }

    public async Task<RespuestaDto> Delete(int id)
    {
        var respuesta = new RespuestaDto();

        try
        {
            var usuarioExistente = await _unitOfWork.UsuarioRepository.GetById(id);

            if (usuarioExistente == null)
            {
                respuesta.Estado = "Error";
                respuesta.Mensaje = $"usuario con ID {id} no encontrado";
                respuesta.Ok = false;
            }

            _unitOfWork.UsuarioRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();

            respuesta.Estado = "Éxito";
            respuesta.Mensaje = $"usuario con ID {id} eliminado correctamente";
            respuesta.Ok = true;

        }
        catch (Exception ex)
        {
            respuesta.Estado = "Error";
            respuesta.Mensaje = $"Ha ocurrido un error al eliminar la usuario con ID {id}: {ex.Message}";
            respuesta.Ok = false;
        }

        return respuesta;
    }
}
