using MediatR;
using Microsoft.EntityFrameworkCore;
using Sttb.Contracts.RequestModels.Perpustakaan;
using Sttb.Entities;

namespace Sttb.Commons.RequestHandlers.Perpustakaan;

public class UpdatePerpustakaanHandler : IRequestHandler<UpdatePerpustakaanRequest, bool>
{
    private readonly ApplicationDbContext _dbContext;

    public UpdatePerpustakaanHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Handle(UpdatePerpustakaanRequest request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.KoleksiPerpustakaans
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity is null)
            throw new KeyNotFoundException($"Koleksi perpustakaan with id '{request.Id}' was not found.");

        entity.Judul = request.Judul;
        entity.Slug = request.Slug;
        entity.Deskripsi = request.Deskripsi ?? string.Empty;
        entity.Penulis = request.Penulis;
        entity.Penerbit = request.Penerbit ?? string.Empty;
        entity.Tahun = request.Tahun;
        entity.Kategori = request.Kategori;
        entity.ThumbnailUrl = request.ThumbnailUrl;
        entity.FileUrl = request.FileUrl;
        entity.IsPublished = request.IsPublished;
        entity.UpdatedAt = DateTime.UtcNow;
        entity.UpdatedBy = "admin";

        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}
