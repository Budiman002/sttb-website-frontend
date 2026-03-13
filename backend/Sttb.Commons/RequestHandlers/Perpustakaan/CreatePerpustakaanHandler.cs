using MediatR;
using Microsoft.EntityFrameworkCore;
using Sttb.Contracts.RequestModels.Perpustakaan;
using Sttb.Entities;
using Sttb.Entities.Entities;

namespace Sttb.Commons.RequestHandlers.Perpustakaan;

public class CreatePerpustakaanHandler : IRequestHandler<CreatePerpustakaanRequest, Guid>
{
    private readonly ApplicationDbContext _dbContext;

    public CreatePerpustakaanHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Handle(CreatePerpustakaanRequest request, CancellationToken cancellationToken)
    {
        var slugExists = await _dbContext.KoleksiPerpustakaans
            .AnyAsync(x => x.Slug == request.Slug, cancellationToken);

        if (slugExists)
            throw new InvalidOperationException($"Slug '{request.Slug}' is already in use.");

        var entity = new KoleksiPerpustakaan
        {
            Id = Guid.NewGuid(),
            Judul = request.Judul,
            Slug = request.Slug,
            Deskripsi = request.Deskripsi ?? string.Empty,
            Penulis = request.Penulis,
            Penerbit = request.Penerbit ?? string.Empty,
            Tahun = request.Tahun,
            Kategori = request.Kategori,
            ThumbnailUrl = request.ThumbnailUrl,
            FileUrl = request.FileUrl,
            IsPublished = request.IsPublished,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = "admin"
        };

        _dbContext.KoleksiPerpustakaans.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
