using MediatR;

namespace Sttb.Contracts.RequestModels.Perpustakaan;

public class DeletePerpustakaanRequest : IRequest<bool>
{
    public Guid Id { get; set; }
}
