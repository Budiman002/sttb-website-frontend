using Sttb.Contracts.RequestModels.Shared;
using Sttb.Contracts.ResponseModels.Berita;

namespace Sttb.Contracts.RequestModels.Berita;

public class GetBeritaListRequest : PagedRequest
{
    public string? Kategori { get; set; }
}
