using MediatR;
using Microsoft.EntityFrameworkCore;
using Sttb.Contracts.RequestModels.Kegiatan;
using Sttb.Entities;

namespace Sttb.Commons.RequestHandlers.Kegiatan;

public class DeleteKegiatanHandler : IRequestHandler<DeleteKegiatanRequest, bool>
{
    private readonly ApplicationDbContext _dbContext;

    public DeleteKegiatanHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Handle(DeleteKegiatanRequest request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Kegiatans
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity is null)
            throw new KeyNotFoundException($"Kegiatan with id '{request.Id}' was not found.");

        _dbContext.Kegiatans.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}
