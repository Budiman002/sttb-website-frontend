using MediatR;
using Microsoft.EntityFrameworkCore;
using Sttb.Contracts.RequestModels.Media;
using Sttb.Entities;

namespace Sttb.Commons.RequestHandlers.Media;

public class DeleteMediaVideoHandler : IRequestHandler<DeleteMediaVideoRequest, bool>
{
    private readonly ApplicationDbContext _dbContext;

    public DeleteMediaVideoHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Handle(DeleteMediaVideoRequest request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.MediaVideos
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity is null)
            throw new KeyNotFoundException($"Media video with id '{request.Id}' was not found.");

        _dbContext.MediaVideos.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}
