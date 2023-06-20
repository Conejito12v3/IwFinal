using Dominio;
using MediatR;
using Persistencia;
using Aplicacion.ClasesDto;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.Productos;

public class ConsultaId
{
    public class ProductoUnico : IRequest<ProductoDto>
    {
        public Guid Id { get; set; }
    }

    public class Manejador : IRequestHandler<ProductoUnico, ProductoDto>
    {
        private readonly EMContext _context;
        private readonly IMapper _mapper;
        public Manejador(EMContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public async Task<ProductoDto> Handle(ProductoUnico request, CancellationToken cancellationToken)
        {
            var producto = await _context.Producto
                .Include(x => x.productor)
                .ThenInclude(x => x.redesSociales)
                .FirstOrDefaultAsync(x => x.productoId == request.Id);

            if (producto == null)
            {
                throw new Exception("No se encontro el producto");
            }

            var productoDto = _mapper.Map<Producto, ProductoDto>(producto);

            return productoDto;
        }
    }
}