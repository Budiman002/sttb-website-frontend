using System.ComponentModel.DataAnnotations;

namespace Sttb.Entities.Entities;

public class Berita
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(300)]
    public string Judul { get; set; } = string.Empty;

    [StringLength(300)]
    public string Slug { get; set; } = string.Empty;

    public string Deskripsi { get; set; } = string.Empty;

    [StringLength(500)]
    public string? ThumbnailUrl { get; set; }

    public bool IsPublished { get; set; } = false;

    [StringLength(50)]
    public string Kategori { get; set; } = string.Empty;

    public string Konten { get; set; } = string.Empty;

    [StringLength(100)]
    public string Penulis { get; set; } = string.Empty;

    public DateTime TanggalPublish { get; set; }

    // Audit
    [StringLength(100)]
    public string CreatedBy { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    [StringLength(100)]
    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
