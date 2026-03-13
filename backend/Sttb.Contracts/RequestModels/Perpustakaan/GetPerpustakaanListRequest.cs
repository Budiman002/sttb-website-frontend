using Sttb.Contracts.RequestModels.Shared;

namespace Sttb.Contracts.RequestModels.Perpustakaan;

public class GetPerpustakaanListRequest : PagedRequest
{
    public string? Kategori { get; set; }
}
