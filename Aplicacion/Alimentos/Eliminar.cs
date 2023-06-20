using MediatR;
using Persistencia;

namespace Aplicacion.Alimentos;

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
            var alimento = await _context.Alimento.FindAsync(request.Id);
            if (alimento == null)
            {
                throw new Exception("No se encontro el alimento");
            }

            _context.Remove(alimento);
            var resultado = await _context.SaveChangesAsync();

            if (resultado > 0)
            {
                return Unit.Value;
            }
            throw new Exception("No se pudo eliminar el alimento");
        }
    }
}