using FluentValidation;
using MediatR;
using Persistencia;
using Dominio;

namespace Aplicacion.Alimentos;

public class Nuevo
{
    public class Ejecuta : IRequest
    {
        public string? nombre { get; set; }
        public string? descripcion { get; set; }
        public string? cantidad { get; set; }
        public string? tipo { get; set; }
    }

    public class EjecutaValidacion : AbstractValidator<Ejecuta>
    {
        public EjecutaValidacion()
        {
            RuleFor(x => x.nombre).NotEmpty();
            RuleFor(x => x.descripcion).NotEmpty();
            RuleFor(x => x.cantidad).NotEmpty();
            RuleFor(x => x.tipo).NotEmpty();
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
            Guid _alimentoId = Guid.NewGuid();
            var alimento = new Alimento
            {
                alimentoId = _alimentoId,
                nombre = request.nombre,
                descripcion = request.descripcion,
                cantidad = request.cantidad,
                tipo = request.tipo
            };

            _context.Alimento.Add(alimento);

            var valor = await _context.SaveChangesAsync();

            if (valor > 0)
            {
                return Unit.Value;
            }

            throw new Exception("No se pudo insertar el alimento");
        }
    }
}