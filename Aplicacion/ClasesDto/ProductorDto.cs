using Dominio;

namespace Aplicacion.ClasesDto;

public class ProductorDto
{
    public Guid productorId { get; set; }
    public string? nombre { get; set; }
    
    // Relaciones:
    public RedesSocialesDto? redesSociales { get; set; }
}