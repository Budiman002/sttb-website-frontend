using MediatR;

namespace Sttb.Contracts.RequestModels.Media;

public class UpdateMediaVideoRequest : IRequest<bool>
{
    public Guid Id { get; set; }
    public string Judul { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Deskripsi { get; set; } = string.Empty;
    public string VideoUrl { get; set; } = string.Empty;
    public string Durasi { get; set; } = string.Empty;
    public string Kategori { get; set; } = string.Empty;
    public string? ThumbnailUrl { get; set; }
    public bool IsPublished { get; set; }
}
