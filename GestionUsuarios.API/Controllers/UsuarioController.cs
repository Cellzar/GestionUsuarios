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

    [HttpPost("registrar")]
    public async Task<ActionResult> RegisterAsync(UsuarioDto model)
    {
        var result = await _userService.RegisterAsync(model);
        return Ok(result);
    }
}
