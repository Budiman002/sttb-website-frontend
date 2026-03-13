using Microsoft.EntityFrameworkCore;
using Sttb.Entities.Entities;

namespace Sttb.Entities;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Berita> Beritas => Set<Berita>();
    public DbSet<Kegiatan> Kegiatans => Set<Kegiatan>();
    public DbSet<MediaArtikel> MediaArtikels => Set<MediaArtikel>();
    public DbSet<MediaVideo> MediaVideos => Set<MediaVideo>();
    public DbSet<KoleksiPerpustakaan> KoleksiPerpustakaans => Set<KoleksiPerpustakaan>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSnakeCaseNamingConvention();
        base.OnConfiguring(optionsBuilder);
    }
}
