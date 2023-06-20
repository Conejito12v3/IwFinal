namespace Dominio;

public class Evento
{
    public Guid eventoId { get; set; }
    public string? nombre { get; set; }
    public int? asistentesEsperados { get; set; }
    public int? asistentes { get; set; }
    public byte[]? imagenEvento { get; set; }
    public DateTime? fecha { get; set; }
}