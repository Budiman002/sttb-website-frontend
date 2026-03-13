using MediatR;
using Sttb.Contracts.ResponseModels.Media;

namespace Sttb.Contracts.RequestModels.Media;

public class GetMediaVideoDetailRequest : IRequest<GetMediaVideoDetailResponse>
{
    public string Slug { get; set; } = string.Empty;
}
