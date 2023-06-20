using MediatR;
using Persistencia;

namespace Aplicacion.Productos;

public class Eliminar
{
    public class Ejecuta : IRequest 
    {
        public Guid Id { get; set; }
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
            var producto = await _context.Producto.FindAsync(request.Id);
            if (producto == null)
            {
                throw new Exception("No se encontro el producto");
            }

            _context.Remove(producto);
            var resultado = await _context.SaveChangesAsync();

            if (resultado > 0)
            {
                return Unit.Value;
            }
            throw new Exception("No se pudo eliminar el curso");
        }
    }
}