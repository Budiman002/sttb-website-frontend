using MediatR;
using Microsoft.EntityFrameworkCore;
using Sttb.Contracts.RequestModels.Berita;
using Sttb.Contracts.ResponseModels.Berita;
using Sttb.Entities;

namespace Sttb.Commons.RequestHandlers.Berita;

public class GetBeritaListHandler : IRequestHandler<GetBeritaListRequest, object>
{
    private readonly ApplicationDbContext _dbContext;

    public GetBeritaListHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<object> Handle(GetBeritaListRequest request, CancellationToken cancellationToken)
    {
        var query = _dbContext.Beritas
            .Where(x => x.IsPublished);

        if (!string.IsNullOrWhiteSpace(request.Kategori))
            query = query.Where(x => x.Kategori == request.Kategori);

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderByDescending(x => x.TanggalPublish)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new BeritaListItem
            {
                Id = x.Id,
                Judul = x.Judul,
                Slug = x.Slug,
                Deskripsi = x.Deskripsi,
                ThumbnailUrl = x.ThumbnailUrl,
                Kategori = x.Kategori,
                Penulis = x.Penulis,
                TanggalPublish = x.TanggalPublish,
                IsPublished = x.IsPublished
            })
            .ToListAsync(cancellationToken);

        return new GetBeritaListResponse
        {
            Items = items,
            TotalCount = totalCount,
            Page = request.Page,
            PageSize = request.PageSize
        };
    }
}
