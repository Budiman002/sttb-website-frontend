using MediatR;
using Microsoft.EntityFrameworkCore;
using Sttb.Contracts.RequestModels.Perpustakaan;
using Sttb.Entities;

namespace Sttb.Commons.RequestHandlers.Perpustakaan;

public class DeletePerpustakaanHandler : IRequestHandler<DeletePerpustakaanRequest, bool>
{
    private readonly ApplicationDbContext _dbContext;

    public DeletePerpustakaanHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Handle(DeletePerpustakaanRequest request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.KoleksiPerpustakaans
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity is null)
            throw new KeyNotFoundException($"Koleksi perpustakaan with id '{request.Id}' was not found.");

        _dbContext.KoleksiPerpustakaans.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}
