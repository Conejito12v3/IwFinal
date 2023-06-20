using System.Net;
using Aplicacion.ClasesDto;
using Aplicacion.ManejadorError;
using AutoMapper;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Productores;

public class Consulta
{
    public class ListaProductores : IRequest<List<Dominio.Productor>> { }

    public class Manejador : IRequestHandler<ListaProductores, List<Dominio.Productor>>
    {
        private readonly EMContext _context;
        private readonly IMapper _mapper;
        
        public Manejador(EMContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Dominio.Productor>> Handle(ListaProductores request, CancellationToken cancellationToken)
        {
            var productores = await _context.Productor
                .Include(x => x.redesSociales)
                .Include(x => x.productos)
                .ToListAsync();

            if (productores == null)
            {
                throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { productores = "No se encontraron productores" });
            }

            var productorDto = _mapper.Map<List<Dominio.Productor>, List<ProductorDto>>(productores);

            return productores;
        }
    }
}