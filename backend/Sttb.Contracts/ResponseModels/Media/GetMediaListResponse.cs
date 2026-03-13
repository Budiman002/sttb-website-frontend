using Sttb.Contracts.ResponseModels.Shared;

namespace Sttb.Contracts.ResponseModels.Media;

public class GetMediaListResponse : PagedResponse<MediaListItem> { }

public class MediaListItem
{
    public Guid Id { get; set; }
    public string Judul { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Deskripsi { get; set; } = string.Empty;
    public string? ThumbnailUrl { get; set; }
    public string Kategori { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string? Penulis { get; set; }
    public bool IsPublished { get; set; }
}
