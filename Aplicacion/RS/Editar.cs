using MediatR;
using Persistencia;

namespace Aplicacion.RS;

public class Editar
{
    public class Ejecuta : IRequest
    {
        public Guid Id { get; set; }
        public string? facebook { get; set; }
        public string? instagram { get; set; }
        public string? twitter { get; set; }
        public string? youtube { get; set; }
        public string? tiktok { get; set; }
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

            rs.facebook = request.facebook ?? rs.facebook;
            rs.instagram = request.instagram ?? rs.instagram;
            rs.twitter = request.twitter ?? rs.twitter;
            rs.youtube = request.youtube ?? rs.youtube;
            rs.tiktok = request.tiktok ?? rs.tiktok;

            var resultado = await _context.SaveChangesAsync();

            if (resultado > 0)
            {
                return Unit.Value;
            }

            throw new Exception("No se pudieron guardar los cambios");
        }
    }
}