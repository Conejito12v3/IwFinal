using Aplicacion.ClasesDto;
using Aplicacion.Productos;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductosController : MiControllerBase
{
    private readonly IMediator _mediator;

    public ProductosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<ActionResult<List<ProductoDto>>> Get()
    {
        return await _mediator.Send(new Aplicacion.Productos.Consulta.ListaProductos());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductoDto>> Detalle(Guid id)
    {
        return await _mediator.Send(new Aplicacion.Productos.ConsultaId.ProductoUnico { Id = id });
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