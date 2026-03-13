using MediatR;
using Microsoft.EntityFrameworkCore;
using Sttb.Contracts.RequestModels.Berita;
using Sttb.Contracts.ResponseModels.Berita;
using Sttb.Entities;

namespace Sttb.Commons.RequestHandlers.Berita;

public class GetBeritaDetailHandler : IRequestHandler<GetBeritaDetailRequest, GetBeritaDetailResponse>
{
    private readonly ApplicationDbContext _dbContext;

    public GetBeritaDetailHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetBeritaDetailResponse> Handle(GetBeritaDetailRequest request, CancellationToken cancellationToken)
    {
        var berita = await _dbContext.Beritas
            .Where(x => x.IsPublished && x.Slug == request.Slug)
            .Select(x => new GetBeritaDetailResponse
            {
                Id = x.Id,
                Judul = x.Judul,
                Slug = x.Slug,
                Deskripsi = x.Deskripsi,
                ThumbnailUrl = x.ThumbnailUrl,
                IsPublished = x.IsPublished,
                Kategori = x.Kategori,
                Konten = x.Konten,
                Penulis = x.Penulis,
                TanggalPublish = x.TanggalPublish,
                CreatedBy = x.CreatedBy,
                CreatedAt = x.CreatedAt,
                UpdatedBy = x.UpdatedBy,
                UpdatedAt = x.UpdatedAt
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (berita is null)
            throw new KeyNotFoundException($"Berita with slug '{request.Slug}' was not found.");

        return berita;
    }
}
