using Dominio;
using FluentValidation;
using MediatR;
using Persistencia;

namespace Aplicacion.Productos;

public class Nuevo
{
    public class Ejecuta : IRequest
    {
        public string? nombre { get; set; }
        public string? descripcion { get; set; }
        public float? precio { get; set; }
        public int? stock { get; set; }
        public DateTime? fechaCreacion { get; set; }
        public Guid? productorId { get; set; }
        public Dominio.Productor? productor { get; set; }
        public Guid? categoriaId { get; set; }
        public CategoriaProducto? categoria { get; set; }
        public byte[]? imagenProducto { get; set; }
    }

    public class EjecutaValidacion : AbstractValidator<Ejecuta>
    {
        public EjecutaValidacion()
        {
            RuleFor(x => x.nombre).NotEmpty();
            RuleFor(x => x.descripcion).NotEmpty();
            RuleFor(x => x.precio).NotEmpty();
            RuleFor(x => x.stock).NotEmpty();
            RuleFor(x => x.fechaCreacion).NotEmpty();
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
            Guid _productoId = Guid.NewGuid();
            if (request.productorId != null && request.productor == null)
            {
                /*var producto = new Producto
                {
                    productoId = _productoId,
                    nombre = request.nombre,
                    descripcion = request.descripcion,
                    precio = request.precio,
                    stock = request.stock,
                    fechaCreacion = request.fechaCreacion,
                    productorId = request.productorId
                };*/

                var producto = (request.categoria != null && request.categoriaId == null) ? 
                    new Producto {
                        productoId = _productoId,
                        nombre = request.nombre,
                        descripcion = request.descripcion,
                        precio = request.precio,
                        stock = request.stock,
                        fechaCreacion = request.fechaCreacion,
                        productorId = request.productorId,
                        //imagenProducto = request.imagenProducto,
                    }
                    :
                    new Producto {
                        productoId = _productoId,
                        nombre = request.nombre,
                        descripcion = request.descripcion,
                        precio = request.precio,
                        stock = request.stock,
                        fechaCreacion = request.fechaCreacion,
                        productorId = request.productorId,
                        categoriaId = request.categoriaId,
                        //imagenProducto = request.imagenProducto,
                    };
                
                _context.Producto.Add(producto);
            }
            else if(request.productor != null && request.productorId == null) 
            {
                Guid _productorId = Guid.NewGuid();
                var producto = new Producto
                {
                    productoId = _productoId,
                    nombre = request.nombre,
                    descripcion = request.descripcion,
                    precio = request.precio,
                    stock = request.stock,
                    fechaCreacion = request.fechaCreacion,
                    productorId = _productoId
                };

                _context.Producto.Add(producto);

                if (request.productor.redesSociales == null)
                {
                    var productor = new Dominio.Productor
                    {
                        productorId = _productoId,
                        nombre = request.productor.nombre
                    };
                    _context.Productor.Add(productor);
                }
                else
                {
                    Guid _redesSocialesId = Guid.NewGuid();

                    var productor = new Dominio.Productor
                    {
                        productorId = _productoId,
                        nombre = request.productor.nombre,
                        redesSocialesId = _redesSocialesId
                    };
                    _context.Productor.Add(productor);

                    var redesSociales = new RedesSociales
                    {
                        redesSocialesId = _redesSocialesId,
                        facebook = request.productor.redesSociales.facebook,
                        instagram = request.productor.redesSociales.instagram,
                        twitter = request.productor.redesSociales.twitter,
                        youtube = request.productor.redesSociales.youtube,
                        tiktok = request.productor.redesSociales.tiktok
                    };
                    _context.RedesSociales.Add(redesSociales);
                }

            }


            var valor = await _context.SaveChangesAsync();

            if (valor > 0)
            {
                return Unit.Value;
            }

            throw new Exception("No se pudo insertar el producto");
        }
    }
}