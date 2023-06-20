using Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Persistencia;

public class EMContext : IdentityDbContext<Usuario>
{
    public EMContext(DbContextOptions options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Productor>()
            .HasOne(x => x.redesSociales)
            .WithOne(x => x.productor)
            .HasForeignKey<RedesSociales>(x => x.productorId);
    }

    public DbSet<Producto> Producto { get; set; }
    public DbSet<Productor> Productor { get; set; }
    public DbSet<RedesSociales> RedesSociales { get; set; }
    public DbSet<CategoriaProducto> CategoriaProducto { get; set; }
    public DbSet<Evento> Evento { get; set; }
    public DbSet<Alimento> Alimento { get; set; }
    public DbSet<Documento> Documento { get; set; }

}