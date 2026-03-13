using Sttb.Contracts.ResponseModels.Shared;

namespace Sttb.Contracts.ResponseModels.Kegiatan;

public class GetKegiatanListResponse : PagedResponse<KegiatanListItem> { }

public class KegiatanListItem
{
    public Guid Id { get; set; }
    public string Judul { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Deskripsi { get; set; } = string.Empty;
    public string? ThumbnailUrl { get; set; }
    public string Lokasi { get; set; } = string.Empty;
    public DateTime TanggalMulai { get; set; }
    public DateTime TanggalSelesai { get; set; }
    public string Status { get; set; } = string.Empty;
    public string Penyelenggara { get; set; } = string.Empty;
    public bool IsPublished { get; set; }
}
