using Aplicacion.Productor;
using Aplicacion.Seguridad;
using Dominio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Authorize]
public class UsuarioController : MiControllerBase
{
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<UsuarioData>> Login (Login.Ejecuta parametros)
    {
        return await Mediator.Send(parametros);
    }

    [HttpPost("registrar")]
    [AllowAnonymous]
    public async Task<ActionResult<UsuarioData>> Registrar (Registrar.Ejecuta parametros)
    {
        return await Mediator.Send(parametros);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<UsuarioData>> DevolverUsuario ()
    {
        return await Mediator.Send(new UsuarioActual.Ejecuta());
    }

    [HttpPut]
    [AllowAnonymous]
    public async Task<ActionResult<UsuarioData>> Actualizar (UsuarioActualizar.Ejecuta parametros)
    {
        return await Mediator.Send(parametros);
    }
}