using MediatR;
using Persistencia;

namespace Aplicacion.RS;

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
            var rs = await _context.RedesSociales.FindAsync(request.Id);
            if (rs == null)
            {
                throw new Exception("No se encontro la red social");
            }

            _context.Remove(rs);
            var resultado = await _context.SaveChangesAsync();

            if (resultado > 0)
            {
                return Unit.Value;
            }
            throw new Exception("No se pudo eliminar la red social");
        }
    }
}