namespace Catalog.Api.Infrastructure.Exceptions;

public sealed class QuantityGreaterThanZeroException : CatalogDomainException
{
    private const string _message = "Item units desired should be greater than zero";

    public QuantityGreaterThanZeroException() : base(_message)
    {
        
    }
}
