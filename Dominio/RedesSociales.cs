namespace Dominio;

public class RedesSociales
{
    public Guid? redesSocialesId { get; set; }
    public string? facebook { get; set; }
    public string? instagram { get; set; }
    public string? twitter { get; set; }
    public string? youtube { get; set; }
    public string? tiktok { get; set; }
    public Guid? productorId { get; set; }
    // Relaciones:
    public Productor? productor { get; set; }
}