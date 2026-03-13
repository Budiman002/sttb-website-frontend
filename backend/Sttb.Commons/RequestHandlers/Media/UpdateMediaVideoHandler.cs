using MediatR;
using Microsoft.EntityFrameworkCore;
using Sttb.Contracts.RequestModels.Media;
using Sttb.Entities;

namespace Sttb.Commons.RequestHandlers.Media;

public class UpdateMediaVideoHandler : IRequestHandler<UpdateMediaVideoRequest, bool>
{
    private readonly ApplicationDbContext _dbContext;

    public UpdateMediaVideoHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Handle(UpdateMediaVideoRequest request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.MediaVideos
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity is null)
            throw new KeyNotFoundException($"Media video with id '{request.Id}' was not found.");

        entity.Judul = request.Judul;
        entity.Slug = request.Slug;
        entity.Deskripsi = request.Deskripsi;
        entity.VideoUrl = request.VideoUrl;
        entity.Durasi = request.Durasi;
        entity.Kategori = request.Kategori;
        entity.ThumbnailUrl = request.ThumbnailUrl;
        entity.IsPublished = request.IsPublished;
        entity.UpdatedAt = DateTime.UtcNow;
        entity.UpdatedBy = "admin";

        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}
