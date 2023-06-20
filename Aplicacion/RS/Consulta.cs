using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.RS;

public class Consulta
{
    public class ListaRS : IRequest<List<RedesSociales>> { }

    public class Manejador : IRequestHandler<ListaRS, List<RedesSociales>>
    {
        private readonly EMContext _context;

        public Manejador(EMContext context)
        {
            _context = context;
        }

        public async Task<List<RedesSociales>> Handle(ListaRS request, CancellationToken cancellationToken)
        {
            var redesSociales = await _context.RedesSociales.ToListAsync();
            return redesSociales;
        }
    }
}