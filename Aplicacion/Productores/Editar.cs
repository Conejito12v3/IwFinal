using System.Net;
using Aplicacion.ManejadorError;
using MediatR;
using Persistencia;

namespace Aplicacion.Productores;

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
            var productor = await _context.Productor.FindAsync(request.Id);
            if (productor == null)
            {
                throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { productor = "No se encontró el productor :(" });
            }

            productor.nombre = request.nombre ?? productor.nombre;

            var resultado = await _context.SaveChangesAsync();

            if (resultado > 0)
            {
                return Unit.Value;
            }

            throw new Exception("No se pudo editar el productor");
        }
    }
}