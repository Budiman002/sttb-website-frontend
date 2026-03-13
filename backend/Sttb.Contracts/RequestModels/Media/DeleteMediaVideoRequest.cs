using MediatR;

namespace Sttb.Contracts.RequestModels.Media;

public class DeleteMediaVideoRequest : IRequest<bool>
{
    public Guid Id { get; set; }
}
