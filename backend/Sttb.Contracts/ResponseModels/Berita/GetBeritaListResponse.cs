using Sttb.Contracts.ResponseModels.Shared;

namespace Sttb.Contracts.ResponseModels.Berita;

public class GetBeritaListResponse : PagedResponse<BeritaListItem> { }

public class BeritaListItem
{
    public Guid Id { get; set; }
    public string Judul { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Deskripsi { get; set; } = string.Empty;
    public string? ThumbnailUrl { get; set; }
    public string Kategori { get; set; } = string.Empty;
    public string Penulis { get; set; } = string.Empty;
    public DateTime TanggalPublish { get; set; }
    public bool IsPublished { get; set; }
}
