using MediatR;
using Microsoft.EntityFrameworkCore;
using Sttb.Contracts.RequestModels.Kegiatan;
using Sttb.Entities;

namespace Sttb.Commons.RequestHandlers.Kegiatan;

public class UpdateKegiatanHandler : IRequestHandler<UpdateKegiatanRequest, bool>
{
    private readonly ApplicationDbContext _dbContext;

    public UpdateKegiatanHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Handle(UpdateKegiatanRequest request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Kegiatans
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity is null)
            throw new KeyNotFoundException($"Kegiatan with id '{request.Id}' was not found.");

        entity.Judul = request.Judul;
        entity.Slug = request.Slug;
        entity.Deskripsi = request.Deskripsi;
        entity.ThumbnailUrl = request.ThumbnailUrl;
        entity.IsPublished = request.IsPublished;
        entity.Lokasi = request.Lokasi;
        entity.TanggalMulai = DateTime.SpecifyKind(request.TanggalMulai, DateTimeKind.Utc);
        entity.TanggalSelesai = DateTime.SpecifyKind(request.TanggalSelesai, DateTimeKind.Utc);
        entity.Status = request.Status;
        entity.Penyelenggara = request.Penyelenggara;
        entity.UpdatedAt = DateTime.UtcNow;
        entity.UpdatedBy = "admin";

        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}
