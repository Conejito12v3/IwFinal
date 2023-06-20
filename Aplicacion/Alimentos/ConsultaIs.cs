using Dominio;
using MediatR;
using Persistencia;

namespace Aplicacion.Alimentos;

public class ConsultaId
{
    public class AlimentoUnico : IRequest<Alimento> 
    { 
        public Guid Id { get; set; }
    }

    public class Manejador : IRequestHandler<AlimentoUnico, Alimento>
    {
        private readonly EMContext _context;
        public Manejador(EMContext context)
        {
            _context = context;
        }

        public async Task<Alimento> Handle(AlimentoUnico request, CancellationToken cancellationToken)
        {
            var alimento = await _context.Alimento.FindAsync(request.Id);

            if (alimento == null)
            {
                throw new Exception("No se encontro el alimento");
            }

            return alimento;
        }
    }
}