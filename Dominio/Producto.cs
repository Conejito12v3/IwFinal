using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio;

public class Producto
{
    public Guid productoId { get; set; }
    public string? nombre { get; set; }
    public string? descripcion { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public float? precio { get; set; }
    public int? stock { get; set; }
    public byte[]? imagenProducto { get; set; }
    public DateTime? fechaCreacion { get; set; }
    public Guid? categoriaId { get; set; }
    public Guid? productorId { get; set; }

    // Relaciones
    public CategoriaProducto? categoria { get; set; }
    public Productor? productor { get; set; }
}