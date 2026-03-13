using MediatR;
using Microsoft.EntityFrameworkCore;
using Sttb.Contracts.RequestModels.Media;
using Sttb.Contracts.ResponseModels.Media;
using Sttb.Entities;

namespace Sttb.Commons.RequestHandlers.Media;

public class GetMediaListHandler : IRequestHandler<GetMediaListRequest, object>
{
    private readonly ApplicationDbContext _dbContext;

    public GetMediaListHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<object> Handle(GetMediaListRequest request, CancellationToken cancellationToken)
    {
        var artikelRaw = new List<(MediaListItem Item, DateTime CreatedAt)>();
        var videoRaw = new List<(MediaListItem Item, DateTime CreatedAt)>();

        if (string.IsNullOrWhiteSpace(request.Type) || request.Type == "artikel")
        {
            artikelRaw = await _dbContext.MediaArtikels
                .Where(x => x.IsPublished)
                .Select(x => new
                {
                    x.Id,
                    x.Judul,
                    x.Slug,
                    x.Deskripsi,
                    x.ThumbnailUrl,
                    x.Kategori,
                    x.Penulis,
                    x.IsPublished,
                    x.CreatedAt
                })
                .ToListAsync(cancellationToken)
                .ContinueWith(t => t.Result.Select(x => (
                    Item: new MediaListItem
                    {
                        Id = x.Id,
                        Judul = x.Judul,
                        Slug = x.Slug,
                        Deskripsi = x.Deskripsi,
                        ThumbnailUrl = x.ThumbnailUrl,
                        Kategori = x.Kategori,
                        Type = "artikel",
                        Penulis = x.Penulis,
                        IsPublished = x.IsPublished
                    },
                    x.CreatedAt
                )).ToList(), cancellationToken);
        }

        if (string.IsNullOrWhiteSpace(request.Type) || request.Type == "video")
        {
            videoRaw = await _dbContext.MediaVideos
                .Where(x => x.IsPublished)
                .Select(x => new
                {
                    x.Id,
                    x.Judul,
                    x.Slug,
                    x.Deskripsi,
                    x.ThumbnailUrl,
                    x.Kategori,
                    x.IsPublished,
                    x.CreatedAt
                })
                .ToListAsync(cancellationToken)
                .ContinueWith(t => t.Result.Select(x => (
                    Item: new MediaListItem
                    {
                        Id = x.Id,
                        Judul = x.Judul,
                        Slug = x.Slug,
                        Deskripsi = x.Deskripsi,
                        ThumbnailUrl = x.ThumbnailUrl,
                        Kategori = x.Kategori,
                        Type = "video",
                        Penulis = null,
                        IsPublished = x.IsPublished
                    },
                    x.CreatedAt
                )).ToList(), cancellationToken);
        }

        var combined = artikelRaw
            .Concat(videoRaw)
            .OrderByDescending(x => x.CreatedAt)
            .ToList();

        var totalCount = combined.Count;

        var items = combined
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => x.Item)
            .ToList();

        return new GetMediaListResponse
        {
            Items = items,
            TotalCount = totalCount,
            Page = request.Page,
            PageSize = request.PageSize
        };
    }
}
