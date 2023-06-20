using MediatR;
using Persistencia;

namespace Aplicacion.Eventos;

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
            var evento = await _context.Evento.FindAsync(request.Id);
            if (evento == null)
            {
                throw new Exception("No se encontro el evento");
            }

            _context.Remove(evento);
            var resultado = await _context.SaveChangesAsync();

            if (resultado > 0)
            {
                return Unit.Value;
            }
            throw new Exception("No se pudo eliminar el evento");
        }
    }
}