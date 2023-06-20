using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.CategoriasProductos;

public class Consulta
{
    public class ListaCategoriasProductos : IRequest<List<CategoriaProducto>> { }

    public class Manejador : IRequestHandler<ListaCategoriasProductos, List<CategoriaProducto>>
    {
        private readonly EMContext _context;
        public Manejador(EMContext context)
        {
            _context = context;
        }

        public async Task<List<CategoriaProducto>> Handle(ListaCategoriasProductos request, CancellationToken cancellationToken)
        {
            var categoriasProductos = await _context.CategoriaProducto
                .Include(x => x.Productos)
                .ToListAsync();
            return categoriasProductos;
        }
    }
}