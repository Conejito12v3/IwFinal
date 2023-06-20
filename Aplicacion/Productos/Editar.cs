using MediatR;
using Persistencia;

namespace Aplicacion.Productos;

public class Editar
{
    public class Ejecuta : IRequest 
    {
        public Guid Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public float? Precio { get; set; }
        public int? Stock { get; set; }
        public Guid? productorId { get; set; }
        public Guid? categoriaProductoId { get; set; }
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

            producto.nombre = request.Nombre ?? producto.nombre;
            producto.descripcion = request.Descripcion ?? producto.descripcion;
            producto.precio = request.Precio ?? producto.precio;
            producto.stock = request.Stock ?? producto.stock;
            producto.productorId = request.productorId ?? producto.productorId;
            producto.categoriaId = request.categoriaProductoId ?? producto.categoriaId;

            var resultado = await _context.SaveChangesAsync();

            if (resultado > 0)
            {
                return Unit.Value;
            }

            throw new Exception("No se pudo editar el producto");
        }
    }
}