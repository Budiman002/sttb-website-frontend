using MediatR;
using Microsoft.EntityFrameworkCore;
using Sttb.Contracts.RequestModels.Berita;
using Sttb.Entities;
using BeritaEntity = Sttb.Entities.Entities.Berita;

namespace Sttb.Commons.RequestHandlers.Berita;

public class CreateBeritaHandler : IRequestHandler<CreateBeritaRequest, Guid>
{
    private readonly ApplicationDbContext _dbContext;

    public CreateBeritaHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Handle(CreateBeritaRequest request, CancellationToken cancellationToken)
    {
        var slugExists = await _dbContext.Beritas
            .AnyAsync(x => x.Slug == request.Slug, cancellationToken);

        if (slugExists)
            throw new InvalidOperationException($"Slug '{request.Slug}' is already in use.");

        var entity = new BeritaEntity
        {
            Id = Guid.NewGuid(),
            Judul = request.Judul,
            Slug = request.Slug,
            Deskripsi = request.Deskripsi,
            Konten = request.Konten,
            Kategori = request.Kategori,
            Penulis = request.Penulis,
            ThumbnailUrl = request.ThumbnailUrl,
            TanggalPublish = DateTime.SpecifyKind(request.TanggalPublish, DateTimeKind.Utc),
            IsPublished = request.IsPublished,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = "admin"
        };

        _dbContext.Beritas.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
