﻿using MassTransit;

using Microsoft.Extensions.Options;
using Catalog.Api.Infrastructure;

namespace Catalog.Api.Services
{
    public sealed class CatalogServices(
        CatalogDbContext context,
        IOptions<CatalogOptions> options,
        ILogger<CatalogServices> logger,
        IPublishEndpoint publish)
    {
        public CatalogDbContext Context { get; } = context;
        public IOptions<CatalogOptions> Options { get; } = options;
        public ILogger<CatalogServices> Logger { get; } = logger;
        public IPublishEndpoint Publish { get; } = publish;
    };
}
