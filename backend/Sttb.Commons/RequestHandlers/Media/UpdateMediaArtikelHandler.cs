using MediatR;
using Microsoft.EntityFrameworkCore;
using Sttb.Contracts.RequestModels.Media;
using Sttb.Entities;

namespace Sttb.Commons.RequestHandlers.Media;

public class UpdateMediaArtikelHandler : IRequestHandler<UpdateMediaArtikelRequest, bool>
{
    private readonly ApplicationDbContext _dbContext;

    public UpdateMediaArtikelHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Handle(UpdateMediaArtikelRequest request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.MediaArtikels
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity is null)
            throw new KeyNotFoundException($"Media artikel with id '{request.Id}' was not found.");

        entity.Judul = request.Judul;
        entity.Slug = request.Slug;
        entity.Deskripsi = request.Deskripsi;
        entity.Konten = request.Konten;
        entity.Kategori = request.Kategori;
        entity.Penulis = request.Penulis;
        entity.ThumbnailUrl = request.ThumbnailUrl;
        entity.IsPublished = request.IsPublished;
        entity.UpdatedAt = DateTime.UtcNow;
        entity.UpdatedBy = "admin";

        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}
