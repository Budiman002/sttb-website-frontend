using MediatR;
using Microsoft.EntityFrameworkCore;
using Sttb.Contracts.RequestModels.Perpustakaan;
using Sttb.Contracts.ResponseModels.Perpustakaan;
using Sttb.Entities;

namespace Sttb.Commons.RequestHandlers.Perpustakaan;

public class GetPerpustakaanDetailHandler : IRequestHandler<GetPerpustakaanDetailRequest, GetPerpustakaanDetailResponse>
{
    private readonly ApplicationDbContext _dbContext;

    public GetPerpustakaanDetailHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetPerpustakaanDetailResponse> Handle(GetPerpustakaanDetailRequest request, CancellationToken cancellationToken)
    {
        var koleksi = await _dbContext.KoleksiPerpustakaans
            .Where(x => x.IsPublished && x.Slug == request.Slug)
            .Select(x => new GetPerpustakaanDetailResponse
            {
                Id = x.Id,
                Judul = x.Judul,
                Slug = x.Slug,
                Deskripsi = x.Deskripsi,
                ThumbnailUrl = x.ThumbnailUrl,
                IsPublished = x.IsPublished,
                Penulis = x.Penulis,
                Penerbit = x.Penerbit,
                Tahun = x.Tahun,
                Kategori = x.Kategori,
                FileUrl = x.FileUrl,
                CreatedBy = x.CreatedBy,
                CreatedAt = x.CreatedAt,
                UpdatedBy = x.UpdatedBy,
                UpdatedAt = x.UpdatedAt
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (koleksi is null)
            throw new KeyNotFoundException($"Koleksi perpustakaan with slug '{request.Slug}' was not found.");

        return koleksi;
    }
}
