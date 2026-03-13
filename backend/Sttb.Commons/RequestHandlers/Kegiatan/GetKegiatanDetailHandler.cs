using MediatR;
using Microsoft.EntityFrameworkCore;
using Sttb.Contracts.RequestModels.Kegiatan;
using Sttb.Contracts.ResponseModels.Kegiatan;
using Sttb.Entities;

namespace Sttb.Commons.RequestHandlers.Kegiatan;

public class GetKegiatanDetailHandler : IRequestHandler<GetKegiatanDetailRequest, GetKegiatanDetailResponse>
{
    private readonly ApplicationDbContext _dbContext;

    public GetKegiatanDetailHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetKegiatanDetailResponse> Handle(GetKegiatanDetailRequest request, CancellationToken cancellationToken)
    {
        var kegiatan = await _dbContext.Kegiatans
            .Where(x => x.IsPublished && x.Slug == request.Slug)
            .Select(x => new GetKegiatanDetailResponse
            {
                Id = x.Id,
                Judul = x.Judul,
                Slug = x.Slug,
                Deskripsi = x.Deskripsi,
                ThumbnailUrl = x.ThumbnailUrl,
                IsPublished = x.IsPublished,
                Lokasi = x.Lokasi,
                TanggalMulai = x.TanggalMulai,
                TanggalSelesai = x.TanggalSelesai,
                Status = x.Status,
                Penyelenggara = x.Penyelenggara,
                CreatedBy = x.CreatedBy,
                CreatedAt = x.CreatedAt,
                UpdatedBy = x.UpdatedBy,
                UpdatedAt = x.UpdatedAt
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (kegiatan is null)
            throw new KeyNotFoundException($"Kegiatan with slug '{request.Slug}' was not found.");

        return kegiatan;
    }
}
