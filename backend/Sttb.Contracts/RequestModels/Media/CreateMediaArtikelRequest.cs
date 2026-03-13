using MediatR;

namespace Sttb.Contracts.RequestModels.Media;

public class CreateMediaArtikelRequest : IRequest<Guid>
{
    public string Judul { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Deskripsi { get; set; } = string.Empty;
    public string Konten { get; set; } = string.Empty;
    public string Kategori { get; set; } = string.Empty;
    public string Penulis { get; set; } = string.Empty;
    public string? ThumbnailUrl { get; set; }
    public bool IsPublished { get; set; }
}
