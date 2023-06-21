using System.Net;
using System.Net.Mail;
using Aplicacion.Productor;
using Aplicacion.Seguridad;
using Dominio;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace WebAPI.Controllers;

[Authorize]
public class UsuarioController : MiControllerBase
{
    [HttpPost("login")]
    [AllowAnonymous]
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

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<MediatR.Unit>>  sendEmail (CodigoVeri.Ejecuta body)
    {
        return await Mediator.Send(body);
    }
    
    [AllowAnonymous]
    [HttpPost("cambiarPassword")]
    public async Task<ActionResult<MediatR.Unit>>  cmabiarPassword (CambiarPassword.Ejecuta parametros)
    {
        return await Mediator.Send(parametros);
    }
}