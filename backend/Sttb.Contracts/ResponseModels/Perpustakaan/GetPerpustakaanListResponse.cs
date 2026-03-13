using Sttb.Contracts.ResponseModels.Shared;

namespace Sttb.Contracts.ResponseModels.Perpustakaan;

public class GetPerpustakaanListResponse : PagedResponse<PerpustakaanListItem> { }

public class PerpustakaanListItem
{
    public Guid Id { get; set; }
    public string Judul { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Deskripsi { get; set; } = string.Empty;
    public string? ThumbnailUrl { get; set; }
    public string Penulis { get; set; } = string.Empty;
    public string Penerbit { get; set; } = string.Empty;
    public int Tahun { get; set; }
    public string Kategori { get; set; } = string.Empty;
    public bool IsPublished { get; set; }
}
