namespace Dominio;

public class Alimento
{
    public Guid alimentoId { get; set; }
    public string? nombre { get; set; }
    public string? descripcion { get; set; }
    public string? cantidad { get; set; }
    public string? tipo { get; set; }
}