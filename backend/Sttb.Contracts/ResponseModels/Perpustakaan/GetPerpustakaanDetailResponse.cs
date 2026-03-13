namespace Sttb.Contracts.ResponseModels.Perpustakaan;

public class GetPerpustakaanDetailResponse
{
    public Guid Id { get; set; }
    public string Judul { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Deskripsi { get; set; } = string.Empty;
    public string? ThumbnailUrl { get; set; }
    public bool IsPublished { get; set; }
    public string Penulis { get; set; } = string.Empty;
    public string Penerbit { get; set; } = string.Empty;
    public int Tahun { get; set; }
    public string Kategori { get; set; } = string.Empty;
    public string? FileUrl { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
