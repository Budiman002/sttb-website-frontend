using Sttb.Contracts.RequestModels.Shared;

namespace Sttb.Contracts.RequestModels.Kegiatan;

public class GetKegiatanListRequest : PagedRequest
{
    public string? Status { get; set; }
}
