using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Eventos;

public class Consulta
{
    public class ListaEventos : IRequest<List<Evento>> { }

    public class Manejador : IRequestHandler<ListaEventos, List<Evento>>
    {
        private readonly EMContext _context;

        public Manejador (EMContext context)
        {
            _context = context;
        }

        public async Task<List<Evento>> Handle(ListaEventos request, CancellationToken cancellationToken)
        {
            var eventos = await _context.Evento.ToListAsync();
            return eventos;
        }
    }
}