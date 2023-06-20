using Aplicacion.CategoriasProductos;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriasProductosController : MiControllerBase
{
    private readonly IMediator _mediator;

    public CategoriasProductosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<CategoriaProducto>>> Get()
    {
        return await _mediator.Send(new Aplicacion.CategoriasProductos.Consulta.ListaCategoriasProductos());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoriaProducto>> Detalle(Guid id)
    {
        return await _mediator.Send(new Aplicacion.CategoriasProductos.ConsultaId.CategoriaProductoUnico { Id = id });
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