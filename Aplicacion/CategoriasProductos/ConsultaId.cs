using Dominio;
using MediatR;
using Persistencia;

namespace Aplicacion.CategoriasProductos;

public class ConsultaId
{
    public class CategoriaProductoUnico : IRequest<CategoriaProducto> 
    { 
        public Guid Id { get; set; }
    }

    public class Manejador : IRequestHandler<CategoriaProductoUnico, CategoriaProducto>
    {
        private readonly EMContext _context;
        public Manejador(EMContext context)
        {
            _context = context;
        }

        public async Task<CategoriaProducto> Handle(CategoriaProductoUnico request, CancellationToken cancellationToken)
        {
            var categoriaProducto = await _context.CategoriaProducto.FindAsync(request.Id);

            if (categoriaProducto == null)
            {
                throw new Exception("No se encontro la categoria del producto");
            }

            return categoriaProducto;
        }
    }
}