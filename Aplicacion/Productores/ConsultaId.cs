using System.Net;
using Aplicacion.ManejadorError;
using Dominio;
using MediatR;
using Persistencia;

namespace Aplicacion.Productores;

public class ConsultaId
{
    public class ProductorUnico : IRequest<Dominio.Productor>
    {
        public Guid Id { get; set; }
    }

    public class Manejador : IRequestHandler<ProductorUnico, Dominio.Productor>
    {
        private readonly EMContext _context;
        public Manejador(EMContext context)
        {
            _context = context;
        }

        public async Task<Dominio.Productor> Handle(ProductorUnico request, CancellationToken cancellationToken)
        {
            var productor = await _context.Productor.FindAsync(request.Id);

            if (productor == null)
            {
                throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { productor = "No se encontró el productor :(" });
            }

            return productor;
        }
    }
}