using Dominio;
using MediatR;
using Persistencia;

namespace Aplicacion.Alimentos;

public class Editar
{
    public class Ejecuta : IRequest
    {
        public Guid Id { get; set; }
        public string? nombre { get; set; }
        public string? descripcion { get; set; }
        public string? cantidad { get; set; }
        public string? tipo { get; set; }
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
            var alimento = await _context.Alimento.FindAsync(request.Id);
            

            if (alimento == null)
            {
                throw new Exception("No se encontro el alimento");
            }

            alimento.nombre = request.nombre ?? alimento.nombre;
            alimento.descripcion = request.descripcion ?? alimento.descripcion;
            alimento.cantidad = request.cantidad ?? alimento.cantidad;
            alimento.tipo = request.tipo ?? alimento.tipo;

            var valor = await _context.SaveChangesAsync();

            if (valor > 0)
            {
                return Unit.Value;
            }

            throw new Exception("No se pudo insertar el alimento");
        }
    }
}