using MediatR;
using Persistencia;

namespace Aplicacion.Eventos;

public class Editar
{
    public class Ejecuta : IRequest
    {
        public Guid Id { get; set; }
        public string? nombre { get; set; }
        public int? asistentesEsperados { get; set; }
        public int? asistentes { get; set; }
        public byte[]? imagenEvento { get; set; }
        public DateTime? fecha { get; set; }
    }

    public class Manejador: IRequestHandler<Ejecuta>
    {
        private readonly EMContext _context;
        public Manejador(EMContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
        {
            var evento = await _context.Evento.FindAsync(request.Id);

            if (evento == null)
            {
                throw new Exception("No se encontro el evento");
            }

            evento.nombre = request.nombre ?? evento.nombre;
            evento.asistentesEsperados = request.asistentesEsperados ?? evento.asistentesEsperados;
            evento.asistentes = request.asistentes ?? evento.asistentes;
            evento.imagenEvento = request.imagenEvento ?? evento.imagenEvento;
            evento.fecha = request.fecha ?? evento.fecha;

            var resultado = await _context.SaveChangesAsync();

            if (resultado > 0)
            {
                return Unit.Value;
            }

            throw new Exception("No se pudo editar el evento");
        }
    }
}