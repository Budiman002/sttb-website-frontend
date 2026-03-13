using MediatR;
using Microsoft.EntityFrameworkCore;
using Sttb.Contracts.RequestModels.Kegiatan;
using Sttb.Contracts.ResponseModels.Kegiatan;
using Sttb.Entities;

namespace Sttb.Commons.RequestHandlers.Kegiatan;

public class GetKegiatanListHandler : IRequestHandler<GetKegiatanListRequest, object>
{
    private readonly ApplicationDbContext _dbContext;

    public GetKegiatanListHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<object> Handle(GetKegiatanListRequest request, CancellationToken cancellationToken)
    {
        var query = _dbContext.Kegiatans
            .Where(x => x.IsPublished);

        if (!string.IsNullOrWhiteSpace(request.Status))
            query = query.Where(x => x.Status == request.Status);

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderByDescending(x => x.TanggalMulai)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new KegiatanListItem
            {
                Id = x.Id,
                Judul = x.Judul,
                Slug = x.Slug,
                Deskripsi = x.Deskripsi,
                ThumbnailUrl = x.ThumbnailUrl,
                Lokasi = x.Lokasi,
                TanggalMulai = x.TanggalMulai,
                TanggalSelesai = x.TanggalSelesai,
                Status = x.Status,
                Penyelenggara = x.Penyelenggara,
                IsPublished = x.IsPublished
            })
            .ToListAsync(cancellationToken);

        return new GetKegiatanListResponse
        {
            Items = items,
            TotalCount = totalCount,
            Page = request.Page,
            PageSize = request.PageSize
        };
    }
}
