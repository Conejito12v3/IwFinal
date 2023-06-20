using Aplicacion.Alimentos;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AlimentosController : MiControllerBase
{
    private readonly IMediator _mediator;

    public AlimentosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<List<Alimento>>> Get()
    {
        return await _mediator.Send(new Aplicacion.Alimentos.Consulta.ListaAlimentos());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Alimento>> Detalle(Guid id)
    {
        return await _mediator.Send(new Aplicacion.Alimentos.ConsultaId.AlimentoUnico { Id = id });
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