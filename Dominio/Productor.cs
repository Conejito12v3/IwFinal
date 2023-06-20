namespace Dominio;

public class Productor
{
    public Guid productorId { get; set; }
    public string? nombre { get; set; }
    public Guid? redesSocialesId { get; set; }
    
    // Relaciones:
    public ICollection<Producto>? productos { get; set; }
    public RedesSociales? redesSociales { get; set; }
}