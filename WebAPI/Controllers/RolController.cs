using Aplicacion.Seguridad;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers;

[AllowAnonymous]
public class RolController : MiControllerBase
{
    [HttpPost("Crear")]
    public async Task<ActionResult<Unit>> Crear(RolNuevo.Ejecuta data) {
        return await Mediator.Send(data);
    }

    [HttpDelete("Eliminar")]
    public async Task<ActionResult<Unit>> Eliminar(RolEliminar.Ejecuta data) {
        return await Mediator.Send(data);
    }

    [HttpGet("Lista")]
    public async Task<ActionResult<List<IdentityRole>>> Lista() {
        return await Mediator.Send(new RolLista.Ejecuta());
    }

    [HttpPost("AgregarRol")]
    public async Task<ActionResult<Unit>> AgregarUsuario(RolUsuarioAgregar.Ejecuta data) {
        return await Mediator.Send(data);
    }

    [HttpPost("EliminarRol")]
    public async Task<ActionResult<Unit>> EliminarUsuario(UsuarioRolEliminar.Ejecuta data) {
        return await Mediator.Send(data);
    }

    [HttpGet("{username}")]
    public async Task<ActionResult<List<string>>> ObtenerRolesPorUsuario(string username) {
        return await Mediator.Send(new UsuarioRolConsulta.Ejecuta { UserName = username });
    }
}