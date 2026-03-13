using MediatR;
using Microsoft.EntityFrameworkCore;
using Sttb.Contracts.RequestModels.Perpustakaan;
using Sttb.Contracts.ResponseModels.Perpustakaan;
using Sttb.Entities;

namespace Sttb.Commons.RequestHandlers.Perpustakaan;

public class GetPerpustakaanListHandler : IRequestHandler<GetPerpustakaanListRequest, object>
{
    private readonly ApplicationDbContext _dbContext;

    public GetPerpustakaanListHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<object> Handle(GetPerpustakaanListRequest request, CancellationToken cancellationToken)
    {
        var query = _dbContext.KoleksiPerpustakaans
            .Where(x => x.IsPublished);

        if (!string.IsNullOrWhiteSpace(request.Kategori))
            query = query.Where(x => x.Kategori == request.Kategori);

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderByDescending(x => x.CreatedAt)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new PerpustakaanListItem
            {
                Id = x.Id,
                Judul = x.Judul,
                Slug = x.Slug,
                Deskripsi = x.Deskripsi,
                ThumbnailUrl = x.ThumbnailUrl,
                Penulis = x.Penulis,
                Penerbit = x.Penerbit,
                Tahun = x.Tahun,
                Kategori = x.Kategori,
                IsPublished = x.IsPublished
            })
            .ToListAsync(cancellationToken);

        return new GetPerpustakaanListResponse
        {
            Items = items,
            TotalCount = totalCount,
            Page = request.Page,
            PageSize = request.PageSize
        };
    }
}
