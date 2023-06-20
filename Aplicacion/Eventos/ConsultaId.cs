using Dominio;
using MediatR;
using Persistencia;

namespace Aplicacion.Eventos;

public class ConsultaId
{
    public class EventoUnico : IRequest<Evento>
    {
        public Guid Id { get; set; }
    }

    public class Manejador : IRequestHandler<EventoUnico, Evento>
    {
        private readonly EMContext _context;
        public Manejador(EMContext context)
        {
            _context = context;
        }

        public async Task<Evento> Handle(EventoUnico request, CancellationToken cancellationToken)
        {
            var evento = await _context.Evento.FindAsync(request.Id);

            if (evento == null)
            {
                throw new Exception("No se encontro el evento");
            }

            return evento;
        }
    }
}