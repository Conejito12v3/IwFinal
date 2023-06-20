using Dominio;

namespace Aplicacion.ClasesDto;

public class ProductoDto
{
    public Guid productoId { get; set; }
    public string? nombre { get; set; }
    public string? descripcion { get; set; }
    public float? precio { get; set; }
    public int? stock { get; set; }
    public byte[]? imagenProducto { get; set; }
    public DateTime? fechaCreacion { get; set; }
    public ProductorDto? productor { get; set; }
    public CategoriaProductoDto? categoria { get; set; }
}