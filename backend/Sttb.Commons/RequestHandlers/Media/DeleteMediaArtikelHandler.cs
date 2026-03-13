using MediatR;
using Microsoft.EntityFrameworkCore;
using Sttb.Contracts.RequestModels.Media;
using Sttb.Entities;

namespace Sttb.Commons.RequestHandlers.Media;

public class DeleteMediaArtikelHandler : IRequestHandler<DeleteMediaArtikelRequest, bool>
{
    private readonly ApplicationDbContext _dbContext;

    public DeleteMediaArtikelHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Handle(DeleteMediaArtikelRequest request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.MediaArtikels
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity is null)
            throw new KeyNotFoundException($"Media artikel with id '{request.Id}' was not found.");

        _dbContext.MediaArtikels.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}
