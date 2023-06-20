using Dominio;
using FluentValidation;
using MediatR;
using Persistencia;

namespace Aplicacion.CategoriasProductos;

public class Nuevo
{
    public class Ejecuta : IRequest
    {
        public string? nombre { get; set; }
    }

    public class EjecutaValidacion : AbstractValidator<Ejecuta>
    {
        public EjecutaValidacion()
        {
            RuleFor(x => x.nombre).NotEmpty();
        }
    }

    public class Manejador : IRequestHandler<Ejecuta>
    {
        private readonly EMContext _context;

        public Manejador(EMContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
        {
            Guid _categoriaProductoId = Guid.NewGuid();
            var categoriaProducto = new CategoriaProducto
            {
                categoriaProductoId = _categoriaProductoId,
                nombre = request.nombre
            };

            _context.CategoriaProducto.Add(categoriaProducto);

            var valor = await _context.SaveChangesAsync();

            if (valor > 0)
            {
                return Unit.Value;
            }

            throw new Exception("No se pudo insertar la categoria de producto");
        }
    }
}