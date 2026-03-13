using MediatR;
using Sttb.Contracts.ResponseModels.Perpustakaan;

namespace Sttb.Contracts.RequestModels.Perpustakaan;

public class GetPerpustakaanDetailRequest : IRequest<GetPerpustakaanDetailResponse>
{
    public string Slug { get; set; } = string.Empty;
}
