using System.Net;
using Aplicacion.ManejadorError;
using Dominio;
using FluentValidation;
using MediatR;
using Persistencia;

namespace Aplicacion.Productores;

public class Nuevo
{
    public class Ejecuta : IRequest
    {
        public string? nombre { get; set; }
        public List<Guid>? ListaProductos { get; set; }
        public Guid? RedesSocialesId { get; set; }
    }

    public class EjecutaValidacion : AbstractValidator<Ejecuta>
    {
        public EjecutaValidacion()
        {
            RuleFor(x => x.nombre).NotEmpty();
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
            Guid _productorId = Guid.NewGuid();
            if(request.RedesSocialesId != null)
            {
                var redesSocialesV = await _context.RedesSociales.FindAsync(request.RedesSocialesId);

                var productor = new Dominio.Productor
                {
                    productorId = _productorId,
                    nombre = request.nombre,
                    redesSocialesId = request.RedesSocialesId.Value,
                    redesSociales = redesSocialesV
                };
                
                _context.Productor.Add(productor);
            }
            else
            {
                
                var productor = new Dominio.Productor
                {
                    productorId = _productorId,
                    nombre = request.nombre
                };
                
                _context.Productor.Add(productor);
            }

            var valor = await _context.SaveChangesAsync();

            if (valor > 0)
            {
                return Unit.Value;
            }

            throw new ManejadorExcepcion(HttpStatusCode.BadGateway, new { productor = "No se pudo insertar el productor :(" });
        }
    }
}