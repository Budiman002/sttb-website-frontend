using MediatR;
using Sttb.Contracts.ResponseModels.Media;

namespace Sttb.Contracts.RequestModels.Media;

public class GetMediaArtikelDetailRequest : IRequest<GetMediaArtikelDetailResponse>
{
    public string Slug { get; set; } = string.Empty;
}
