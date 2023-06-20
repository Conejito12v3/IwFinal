using Aplicacion.Eventos;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventosController :  MiControllerBase
{
    private readonly IMediator _mediator;

    public EventosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<Evento>>> Get()
    {
        return await _mediator.Send(new Consulta.ListaEventos());
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Evento>> Detalle(Guid id)
    {
        return await _mediator.Send(new ConsultaId.EventoUnico { Id = id });
    }

    [HttpPost]
    public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
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