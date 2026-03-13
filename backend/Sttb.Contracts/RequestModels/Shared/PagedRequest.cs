using MediatR;

namespace Sttb.Contracts.RequestModels.Shared;

public abstract class PagedRequest : IRequest<object>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 9;
}
