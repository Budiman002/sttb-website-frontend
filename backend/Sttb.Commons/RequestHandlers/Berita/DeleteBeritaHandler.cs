using MediatR;
using Microsoft.EntityFrameworkCore;
using Sttb.Contracts.RequestModels.Berita;
using Sttb.Entities;

namespace Sttb.Commons.RequestHandlers.Berita;

public class DeleteBeritaHandler : IRequestHandler<DeleteBeritaRequest, bool>
{
    private readonly ApplicationDbContext _dbContext;

    public DeleteBeritaHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Handle(DeleteBeritaRequest request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Beritas
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity is null)
            throw new KeyNotFoundException($"Berita with id '{request.Id}' was not found.");

        _dbContext.Beritas.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}
