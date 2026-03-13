using MediatR;

namespace Sttb.Contracts.RequestModels.Berita;

public class DeleteBeritaRequest : IRequest<bool>
{
    public Guid Id { get; set; }
}
