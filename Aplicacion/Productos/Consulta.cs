using Aplicacion.ClasesDto;
using AutoMapper;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Productos;

public class Consulta
{
    public class ListaProductos: IRequest<List<ProductoDto>> { }

    public class Manejador : IRequestHandler<ListaProductos, List<ProductoDto>>
    {
        private readonly EMContext _context;
        private readonly IMapper _mapper;

        public Manejador(EMContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ProductoDto>> Handle(ListaProductos request, CancellationToken cancellationToken)
        {
            var productos = await _context.Producto
                .Include(x => x.categoria)
                .Include(x => x.productor)
                .ThenInclude(x => x.redesSociales)
                .ToListAsync();

            var productoDto = _mapper.Map<List<Producto>, List<ProductoDto>>(productos);

            return productoDto;
        }
    } 
}