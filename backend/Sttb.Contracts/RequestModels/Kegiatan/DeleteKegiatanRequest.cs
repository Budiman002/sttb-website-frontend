using MediatR;

namespace Sttb.Contracts.RequestModels.Kegiatan;

public class DeleteKegiatanRequest : IRequest<bool>
{
    public Guid Id { get; set; }
}
