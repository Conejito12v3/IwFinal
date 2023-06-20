using MediatR;
using Persistencia;

namespace Aplicacion.CategoriasProductos;

public class Editar 
{
    public class Ejecuta : IRequest 
    {
        public Guid Id { get; set; }
        public string? nombre { get; set; }
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
            var categoriaProducto = await _context.CategoriaProducto.FindAsync(request.Id);
            
            if (categoriaProducto == null)
            {
                throw new Exception("No se encontro la categoria del producto");
            }

            categoriaProducto.nombre = request.nombre ?? categoriaProducto.nombre;

            var resultado = await _context.SaveChangesAsync();

            if (resultado > 0)
            {
                return Unit.Value;
            }

            throw new Exception("No se pudo editar la categoria del producto");
        }
    }
}