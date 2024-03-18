using GestionUsuarios.APPLICATION.Common.Interfaces;
using GestionUsuarios.APPLICATION.Services;
using GestionUsuarios.DOMAIN.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GestionUsuarios.API.Controllers;

public class UsuarioController : BaseApiController
{
    private readonly IUsuarioService _userService;
    public UsuarioController(IUsuarioService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Crea una nuevo usuario.
    /// </summary>
    /// <param name="usuario">El usuario a crear.</param>
    /// <returns>Una respuesta HTTP con el resultado de la operación.</returns>
    [HttpPost("registrar")]
    public async Task<ActionResult> RegisterAsync(UsuarioDto usuario)
    {
        var result = await _userService.RegisterAsync(usuario);
        return Ok(result);
    }

    /// <summary>
    /// Login usuario.
    /// </summary>
    /// <param name="usuario">El usuario a logear.</param>
    /// <returns>Una respuesta HTTP con el resultado de la operación.</returns>
    [HttpPost("login")]
    public async Task<IActionResult> GetTokenAsync(UsuarioDto usuario)
    {
        var result = await _userService.GetTokenAsync(usuario);
        return Ok(result);
    }

    /// <summary>
    /// Obtiene todos las usuarios.
    /// </summary>
    /// <returns>Una respuesta HTTP con el resultado de la operación.</returns>
    [HttpGet]
    public async Task<ActionResult<RespuestaDto>> Get()
    {
        var result = await _userService.Get();
        return Ok(result);
    }

    /// <summary>
    /// Obtiene una usuario por su ID.
    /// </summary>
    /// <param name="id">El ID de la usuario a obtener.</param>
    /// <returns>Una respuesta HTTP con el resultado de la operación.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<RespuestaDto>> GetById(int id)
    {
        var result = await _userService.GetById(id);
        return Ok(result);
    }

    /// <summary>
    /// Actualiza una persona existente.
    /// </summary>
    /// <param name="id">El ID del usuario a actualizar.</param>
    /// <param name="persona">El objeto Usuario con los datos actualizados.</param>
    /// <returns>Una respuesta HTTP con el resultado de la operación.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<RespuestaDto>> Update(int id, [FromBody] UsuarioDto usuarioDto)
    {
        var result = await _userService.Update(id, usuarioDto);
        return Ok(result);
    }


    /// <summary>
    /// Elimina una usuario existente.
    /// </summary>
    /// <param name="id">El ID de la usuario a eliminar.</param>
    /// <returns>Una respuesta HTTP con el resultado de la operación.</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<RespuestaDto>> Delete(int id)
    {
        var result = await _userService.Delete(id);
        return Ok(result);
    }
}
