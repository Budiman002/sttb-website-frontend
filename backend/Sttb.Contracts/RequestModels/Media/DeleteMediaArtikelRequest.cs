using MediatR;

namespace Sttb.Contracts.RequestModels.Media;

public class DeleteMediaArtikelRequest : IRequest<bool>
{
    public Guid Id { get; set; }
}
