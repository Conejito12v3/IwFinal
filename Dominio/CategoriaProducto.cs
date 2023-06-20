namespace Dominio;

public class CategoriaProducto
{
    public Guid? categoriaProductoId { get; set; }
    public string? nombre { get; set; }

    // Relaciones:
    public ICollection<Producto>? Productos { get; set; }
}