using MediatR;
using Microsoft.EntityFrameworkCore;
using Sttb.Contracts.RequestModels.Berita;
using Sttb.Entities;

namespace Sttb.Commons.RequestHandlers.Berita;

public class UpdateBeritaHandler : IRequestHandler<UpdateBeritaRequest, bool>
{
    private readonly ApplicationDbContext _dbContext;

    public UpdateBeritaHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Handle(UpdateBeritaRequest request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Beritas
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity is null)
            throw new KeyNotFoundException($"Berita with id '{request.Id}' was not found.");

        entity.Judul = request.Judul;
        entity.Slug = request.Slug;
        entity.Deskripsi = request.Deskripsi;
        entity.Konten = request.Konten;
        entity.Kategori = request.Kategori;
        entity.Penulis = request.Penulis;
        entity.ThumbnailUrl = request.ThumbnailUrl;
        entity.TanggalPublish = DateTime.SpecifyKind(request.TanggalPublish, DateTimeKind.Utc);
        entity.IsPublished = request.IsPublished;
        entity.UpdatedAt = DateTime.UtcNow;
        entity.UpdatedBy = "admin";

        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}
