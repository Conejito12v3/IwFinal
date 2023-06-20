using Dominio;
using FluentValidation;
using MediatR;
using Persistencia;

namespace Aplicacion.RS;

public class Nuevo
{
    public class Ejecuta : IRequest<Guid>
    {
        public string? facebook { get; set; }
        public string? instagram { get; set; }
        public string? twitter { get; set; }
        public string? youtube { get; set; }
        public string? tiktok { get; set; }
    }

    public class Manejador : IRequestHandler<Ejecuta, Guid>
    {
        private readonly EMContext _context;

        public Manejador(EMContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(Ejecuta request, CancellationToken cancellationToken)
        {
            Guid _rsId = Guid.NewGuid();
            var rs = new RedesSociales
            {
                redesSocialesId = _rsId,
                facebook = request.facebook,
                instagram = request.instagram,
                twitter = request.twitter,
                youtube = request.youtube,
                tiktok = request.tiktok
            };

            _context.RedesSociales.Add(rs);

            var valor = await _context.SaveChangesAsync();

            if (valor > 0)
            {
                return _rsId;
            }

            throw new Exception("No se pudo insertar las redes sociales");
        }
    }
}