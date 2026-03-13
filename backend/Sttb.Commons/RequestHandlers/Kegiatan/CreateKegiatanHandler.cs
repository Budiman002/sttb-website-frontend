using MediatR;
using Microsoft.EntityFrameworkCore;
using Sttb.Contracts.RequestModels.Kegiatan;
using Sttb.Entities;
using KegiatanEntity = Sttb.Entities.Entities.Kegiatan;

namespace Sttb.Commons.RequestHandlers.Kegiatan;

public class CreateKegiatanHandler : IRequestHandler<CreateKegiatanRequest, Guid>
{
    private readonly ApplicationDbContext _dbContext;

    public CreateKegiatanHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Handle(CreateKegiatanRequest request, CancellationToken cancellationToken)
    {
        var slugExists = await _dbContext.Kegiatans
            .AnyAsync(x => x.Slug == request.Slug, cancellationToken);

        if (slugExists)
            throw new InvalidOperationException($"Slug '{request.Slug}' is already in use.");

        var entity = new KegiatanEntity
        {
            Id = Guid.NewGuid(),
            Judul = request.Judul,
            Slug = request.Slug,
            Deskripsi = request.Deskripsi,
            ThumbnailUrl = request.ThumbnailUrl,
            IsPublished = request.IsPublished,
            Lokasi = request.Lokasi,
            TanggalMulai = DateTime.SpecifyKind(request.TanggalMulai, DateTimeKind.Utc),
            TanggalSelesai = DateTime.SpecifyKind(request.TanggalSelesai, DateTimeKind.Utc),
            Status = request.Status,
            Penyelenggara = request.Penyelenggara,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = "admin"
        };

        _dbContext.Kegiatans.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
