using Dominio;
using FluentValidation;
using MediatR;
using Persistencia;

namespace Aplicacion.Eventos;

public class Nuevo
{
    public class Ejecuta : IRequest
    {
        public string? nombre { get; set; }
        public int? asistentesEsperados { get; set; }
        public int? asistentes { get; set; }
        public byte[]? imagenEvento { get; set; }
        public DateTime? fecha { get; set; }
    }

    public class EjecutaValidacion : AbstractValidator<Ejecuta>
    {
        public EjecutaValidacion()
        {
            RuleFor(x => x.nombre).NotEmpty();
            RuleFor(x => x.asistentesEsperados).NotEmpty();
            RuleFor(x => x.asistentes).NotEmpty();
            RuleFor(x => x.fecha).NotEmpty();
        }
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
            Guid _eventoId = Guid.NewGuid();
            var evento = new Evento 
            {
                eventoId = _eventoId,
                nombre = request.nombre,
                asistentesEsperados = request.asistentesEsperados,
                asistentes = request.asistentes,
                imagenEvento = request.imagenEvento,
                fecha = request.fecha
            };
            
            _context.Evento.Add(evento);

            var valor = await _context.SaveChangesAsync();

            if (valor > 0)
            {
                return Unit.Value;
            }

            throw new Exception("No se pudo registrar el evento");
        }
    }
}