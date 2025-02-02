﻿using MediatR;
using Moonglade.Data.Entities;
using Moonglade.Data.Infrastructure;

namespace Moonglade.Pingback;

public record ClearPingbackCommand : IRequest;

public class ClearPingbackCommandHandler : AsyncRequestHandler<ClearPingbackCommand>
{
    private readonly IRepository<PingbackEntity> _pingbackRepo;

    public ClearPingbackCommandHandler(IRepository<PingbackEntity> pingbackRepo) => _pingbackRepo = pingbackRepo;

    protected override Task Handle(ClearPingbackCommand request, CancellationToken cancellationToken) =>
        _pingbackRepo.Clear(cancellationToken);
}