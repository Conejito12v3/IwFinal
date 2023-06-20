using System.Net;
using Aplicacion.ManejadorError;
using MediatR;
using Persistencia;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.Productores;

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
            var productor = await _context.Productor.OrderBy(e => e.productorId).Include(e => e.redesSociales).Include(e => e.productos).FirstOrDefaultAsync(e => e.productorId == request.Id);
            if (productor == null)
            {
                throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { productor = "No se encontró el productor :(" });
            }

            if (productor.redesSociales != null)
            {
                _context.RemoveRange(productor.redesSociales);
            }

            if (productor.productos != null)
            {
                _context.RemoveRange(productor.productos);
            }

            _context.Remove(productor);
            var resultado = await _context.SaveChangesAsync();

            if (resultado > 0)
            {
                return Unit.Value;
            }
            throw new Exception("No se pudo eliminar el curso");
        }
    }
}