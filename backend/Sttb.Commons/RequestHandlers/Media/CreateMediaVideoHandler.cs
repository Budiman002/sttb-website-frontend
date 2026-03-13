using MediatR;
using Microsoft.EntityFrameworkCore;
using Sttb.Contracts.RequestModels.Media;
using Sttb.Entities;
using Sttb.Entities.Entities;

namespace Sttb.Commons.RequestHandlers.Media;

public class CreateMediaVideoHandler : IRequestHandler<CreateMediaVideoRequest, Guid>
{
    private readonly ApplicationDbContext _dbContext;

    public CreateMediaVideoHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Handle(CreateMediaVideoRequest request, CancellationToken cancellationToken)
    {
        var slugExists = await _dbContext.MediaVideos
            .AnyAsync(x => x.Slug == request.Slug, cancellationToken);

        if (slugExists)
            throw new InvalidOperationException($"Slug '{request.Slug}' is already in use.");

        var entity = new MediaVideo
        {
            Id = Guid.NewGuid(),
            Judul = request.Judul,
            Slug = request.Slug,
            Deskripsi = request.Deskripsi,
            VideoUrl = request.VideoUrl,
            Durasi = request.Durasi,
            Kategori = request.Kategori,
            ThumbnailUrl = request.ThumbnailUrl,
            IsPublished = request.IsPublished,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = "admin"
        };

        _dbContext.MediaVideos.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
