﻿namespace Catalog.Api.Infrastructure.Exceptions;

public sealed class EmptyStockException : CatalogDomainException
{
    public EmptyStockException(string name) : base($"Empty stock, product item {name} is sold out")
    {

    }
}
