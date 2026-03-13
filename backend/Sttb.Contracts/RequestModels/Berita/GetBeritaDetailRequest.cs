using MediatR;
using Sttb.Contracts.ResponseModels.Berita;

namespace Sttb.Contracts.RequestModels.Berita;

public class GetBeritaDetailRequest : IRequest<GetBeritaDetailResponse>
{
    public string Slug { get; set; } = string.Empty;
}
