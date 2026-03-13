using MediatR;
using Microsoft.EntityFrameworkCore;
using Sttb.Contracts.RequestModels.Media;
using Sttb.Contracts.ResponseModels.Media;
using Sttb.Entities;

namespace Sttb.Commons.RequestHandlers.Media;

public class GetMediaVideoDetailHandler : IRequestHandler<GetMediaVideoDetailRequest, GetMediaVideoDetailResponse>
{
    private readonly ApplicationDbContext _dbContext;

    public GetMediaVideoDetailHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetMediaVideoDetailResponse> Handle(GetMediaVideoDetailRequest request, CancellationToken cancellationToken)
    {
        var video = await _dbContext.MediaVideos
            .Where(x => x.IsPublished && x.Slug == request.Slug)
            .Select(x => new GetMediaVideoDetailResponse
            {
                Id = x.Id,
                Judul = x.Judul,
                Slug = x.Slug,
                Deskripsi = x.Deskripsi,
                ThumbnailUrl = x.ThumbnailUrl,
                IsPublished = x.IsPublished,
                VideoUrl = x.VideoUrl,
                Durasi = x.Durasi,
                Kategori = x.Kategori,
                CreatedBy = x.CreatedBy,
                CreatedAt = x.CreatedAt,
                UpdatedBy = x.UpdatedBy,
                UpdatedAt = x.UpdatedAt
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (video is null)
            throw new KeyNotFoundException($"Media video with slug '{request.Slug}' was not found.");

        return video;
    }
}
