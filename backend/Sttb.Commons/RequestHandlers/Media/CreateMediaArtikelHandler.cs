using MediatR;
using Microsoft.EntityFrameworkCore;
using Sttb.Contracts.RequestModels.Media;
using Sttb.Entities;
using Sttb.Entities.Entities;

namespace Sttb.Commons.RequestHandlers.Media;

public class CreateMediaArtikelHandler : IRequestHandler<CreateMediaArtikelRequest, Guid>
{
    private readonly ApplicationDbContext _dbContext;

    public CreateMediaArtikelHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Handle(CreateMediaArtikelRequest request, CancellationToken cancellationToken)
    {
        var slugExists = await _dbContext.MediaArtikels
            .AnyAsync(x => x.Slug == request.Slug, cancellationToken);

        if (slugExists)
            throw new InvalidOperationException($"Slug '{request.Slug}' is already in use.");

        var entity = new MediaArtikel
        {
            Id = Guid.NewGuid(),
            Judul = request.Judul,
            Slug = request.Slug,
            Deskripsi = request.Deskripsi,
            Konten = request.Konten,
            Kategori = request.Kategori,
            Penulis = request.Penulis,
            ThumbnailUrl = request.ThumbnailUrl,
            IsPublished = request.IsPublished,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = "admin"
        };

        _dbContext.MediaArtikels.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
