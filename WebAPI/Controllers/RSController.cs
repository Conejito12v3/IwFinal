using Aplicacion.RS;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RSController : MiControllerBase
{
    private readonly IMediator _mediator;

    public RSController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<RedesSociales>>> Get()
    {
        return await _mediator.Send(new Consulta.ListaRS());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RedesSociales>> Detalle(Guid id)
    {
        return await _mediator.Send(new ConsultaId.RSUnicos { Id = id });
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Guid>> Crear(Nuevo.Ejecuta data)
    {
        return await _mediator.Send(data);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Unit>> Editar(Guid id, Editar.Ejecuta data)
    {
        data.Id = id;
        return await _mediator.Send(data);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Unit>> Eliminar(Guid id)
    {
        return await _mediator.Send(new Eliminar.Ejecuta { Id = id });
    }
}