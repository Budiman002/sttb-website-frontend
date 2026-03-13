using MediatR;
using Microsoft.EntityFrameworkCore;
using Sttb.Contracts.RequestModels.Media;
using Sttb.Contracts.ResponseModels.Media;
using Sttb.Entities;

namespace Sttb.Commons.RequestHandlers.Media;

public class GetMediaArtikelDetailHandler : IRequestHandler<GetMediaArtikelDetailRequest, GetMediaArtikelDetailResponse>
{
    private readonly ApplicationDbContext _dbContext;

    public GetMediaArtikelDetailHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetMediaArtikelDetailResponse> Handle(GetMediaArtikelDetailRequest request, CancellationToken cancellationToken)
    {
        var artikel = await _dbContext.MediaArtikels
            .Where(x => x.IsPublished && x.Slug == request.Slug)
            .Select(x => new GetMediaArtikelDetailResponse
            {
                Id = x.Id,
                Judul = x.Judul,
                Slug = x.Slug,
                Deskripsi = x.Deskripsi,
                ThumbnailUrl = x.ThumbnailUrl,
                IsPublished = x.IsPublished,
                Konten = x.Konten,
                Kategori = x.Kategori,
                Penulis = x.Penulis,
                CreatedBy = x.CreatedBy,
                CreatedAt = x.CreatedAt,
                UpdatedBy = x.UpdatedBy,
                UpdatedAt = x.UpdatedAt
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (artikel is null)
            throw new KeyNotFoundException($"Media artikel with slug '{request.Slug}' was not found.");

        return artikel;
    }
}
