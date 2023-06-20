using Dominio;
using MediatR;
using Persistencia;

namespace Aplicacion.RS;

public class ConsultaId
{
    public class RSUnicos : IRequest<RedesSociales>
    {
        public Guid Id { get; set; }
    }

    public class Manejador : IRequestHandler<RSUnicos, RedesSociales>
    {
        private readonly EMContext _context;
        public Manejador(EMContext context)
        {
            _context = context;
        }

        public async Task<RedesSociales> Handle(RSUnicos request, CancellationToken cancellationToken)
        {
            var rs = await _context.RedesSociales.FindAsync(request.Id);

            if (rs == null)
            {
                throw new Exception("No se encontro la red social");
            }

            return rs;
        }
    }
}