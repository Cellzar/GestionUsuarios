using GestionUsuarios.APPLICATION.Common.Interfaces;
using GestionUsuarios.DOMAIN.Dto;
using GestionUsuarios.DOMAIN.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GestionUsuarios.APPLICATION.Services;

public class UsuarioService : IUsuarioService
{
    private readonly JWT _jwt;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher<Usuario> _passwordHasher;
    public UsuarioService(IUnitOfWork unitOfWork, IOptions<JWT> jwt,
        IPasswordHasher<Usuario> passwordHasher)
    {
        _jwt = jwt.Value;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public Task<DatosUsuarioDto> GetTokenAsync(UsuarioDto model)
    {
        throw new NotImplementedException();
    }

    public async Task<RespuestaDto> RegisterAsync(UsuarioDto usuarioDto)
    {
        RespuestaDto respuesta = new RespuestaDto();
        var usuario = new Usuario
        {
            UsuarioNombre = usuarioDto.UsuarioNombre,
            FechaCreacion = DateTime.Now
        };

        usuario.Pass = _passwordHasher.HashPassword(usuario, usuarioDto.Pass);

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
}
