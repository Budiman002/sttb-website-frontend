using Sttb.Contracts.RequestModels.Shared;

namespace Sttb.Contracts.RequestModels.Media;

public class GetMediaListRequest : PagedRequest
{
    public string? Type { get; set; }
}
