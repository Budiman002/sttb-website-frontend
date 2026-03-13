using MediatR;
using Sttb.Contracts.ResponseModels.Kegiatan;

namespace Sttb.Contracts.RequestModels.Kegiatan;

public class GetKegiatanDetailRequest : IRequest<GetKegiatanDetailResponse>
{
    public string Slug { get; set; } = string.Empty;
}
