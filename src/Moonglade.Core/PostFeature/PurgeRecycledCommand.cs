﻿using Moonglade.Caching;
using Moonglade.Data.Spec;

namespace Moonglade.Core.PostFeature;

public record PurgeRecycledCommand : IRequest;

public class PurgeRecycledCommandHandler : AsyncRequestHandler<PurgeRecycledCommand>
{
    private readonly IBlogCache _cache;
    private readonly IRepository<PostEntity> _postRepo;

    public PurgeRecycledCommandHandler(IBlogCache cache, IRepository<PostEntity> postRepo)
    {
        _cache = cache;
        _postRepo = postRepo;
    }

    protected override async Task Handle(PurgeRecycledCommand request, CancellationToken cancellationToken)
    {
        var spec = new PostSpec(true);
        var posts = await _postRepo.ListAsync(spec);
        await _postRepo.DeleteAsync(posts);

        foreach (var guid in posts.Select(p => p.Id))
        {
            _cache.Remove(CacheDivision.Post, guid.ToString());
        }
    }
}