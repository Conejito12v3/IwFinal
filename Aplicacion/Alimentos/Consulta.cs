using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Alimentos;

public class Consulta
{
    public class ListaAlimentos : IRequest<List<Alimento>> { }

    public class Manejador : IRequestHandler<ListaAlimentos, List<Alimento>>
    {
        private readonly EMContext _context;
        public Manejador(EMContext context)
        {
            _context = context;
        }

        public async Task<List<Alimento>> Handle(ListaAlimentos request, CancellationToken cancellationToken)
        {
            var alimentos = await _context.Alimento.ToListAsync();
            return alimentos;
        }
    }
}