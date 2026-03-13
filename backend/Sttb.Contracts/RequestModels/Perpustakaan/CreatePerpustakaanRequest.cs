using MediatR;

namespace Sttb.Contracts.RequestModels.Perpustakaan;

public class CreatePerpustakaanRequest : IRequest<Guid>
{
    public string Judul { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? Deskripsi { get; set; }
    public string Penulis { get; set; } = string.Empty;
    public string? Penerbit { get; set; }
    public int Tahun { get; set; }
    public string Kategori { get; set; } = string.Empty;
    public string? ThumbnailUrl { get; set; }
    public string? FileUrl { get; set; }
    public bool IsPublished { get; set; }
}
