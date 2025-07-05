using Elastic.Clients.Elasticsearch;

using Microsoft.AspNetCore.Http.HttpResults;

using Search.Api.Infrastructure.Extensions;
using Search.Api.Models;

var builder = WebApplication.CreateBuilder(args);

builder.BrokerConfigure();
builder.ElasticSearchConfigure();
builder.BindAppSettings();

builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.MapGet("/api/v1/Fuzzy", SearchFuzzyItems);
app.MapGet("/api/v1/Proximity", SearchProximityItems);
app.MapGet("/api/v1/Term", SearchTermItems);
app.MapGet("/api/v1/FullText", SearchFullTextItems);
app.MapGet("/api/v1/MultiMatch", SearchMultiMatchItems);
app.MapGet("/api/v1/Boolean", SearchBooleanItems);

app.Run();

static async Task<Results<Ok<IReadOnlyCollection<CatalogItemIndex>>, NotFound>> SearchFuzzyItems(string qr, ElasticsearchClient elasticsearch)
{
    var response = await elasticsearch.SearchAsync<CatalogItemIndex>(s => s
        .Index(CatalogItemIndex.IndexName)
        .From(0)
        .Size(10)
        .Query(q => q
            .Bool(b => b
                .Should(
                    sh => sh.Fuzzy(f => f.Field(x => x.Description).Value(qr)),
                    sh => sh.Fuzzy(f => f.Field(x => x.Name).Value(qr)),
                    sh => sh.Fuzzy(f => f.Field(x => x.Id).Value(qr))
                )
            )
        )
    );
    if (response.IsValidResponse)
        return TypedResults.Ok(response.Documents);

    return TypedResults.NotFound();

}
static async Task<Results<Ok<IReadOnlyCollection<CatalogItemIndex>>, NotFound>> SearchProximityItems(string qr, ElasticsearchClient elasticsearch)
{
    var response = await elasticsearch.SearchAsync<CatalogItemIndex>(s => s
        .Index(CatalogItemIndex.IndexName)
        .From(0)
        .Size(10)
        .Query(q => q
            .MatchPhrase(mp => mp
                .Field(f => f.Description)
                .Query(qr)
                .Slop(2) // فاصله مجاز بین کلمات، مثلا 2 کلمه
            )
        )
    );

    if (response.IsValidResponse)
        return TypedResults.Ok(response.Documents);

    return TypedResults.NotFound();
}
static async Task<Results<Ok<IReadOnlyCollection<CatalogItemIndex>>, NotFound>> SearchTermItems(string term, ElasticsearchClient elasticsearch)
{
    var response = await elasticsearch.SearchAsync<CatalogItemIndex>(s => s
        .Index(CatalogItemIndex.IndexName)
        .From(0)
        .Size(10)
        .Query(q => q
            .Term(t => t.Field(f => f.Name).Value(term))
        )
    );

    if (response.IsValidResponse)
        return TypedResults.Ok(response.Documents);

    return TypedResults.NotFound();
}
static async Task<Results<Ok<IReadOnlyCollection<CatalogItemIndex>>, NotFound>> SearchFullTextItems(string qr, ElasticsearchClient elasticsearch)
{
    var response = await elasticsearch.SearchAsync<CatalogItemIndex>(s => s
        .Index(CatalogItemIndex.IndexName)
        .From(0)
        .Size(10)
        .Query(q => q
            .Match(m => m
                .Field(f => f.Description)
                .Query(qr)
            )
        )
    );

    if (response.IsValidResponse)
        return TypedResults.Ok(response.Documents);

    return TypedResults.NotFound();
}
static async Task<Results<Ok<IReadOnlyCollection<CatalogItemIndex>>, NotFound>> SearchMultiMatchItems(string qr, ElasticsearchClient elasticsearch)
{
    var response = await elasticsearch.SearchAsync<CatalogItemIndex>(s => s
        .Index(CatalogItemIndex.IndexName)
        .From(0)
        .Size(10)
        .Query(q => q
            .MultiMatch(mm => mm
                .Fields("name").Fields("Description")
                .Query(qr)
            )
        )
    );

    if (response.IsValidResponse)
        return TypedResults.Ok(response.Documents);

    return TypedResults.NotFound();
}
static async Task<Results<Ok<IReadOnlyCollection<CatalogItemIndex>>, NotFound>> SearchBooleanItems(string qrName, string qrDescription, ElasticsearchClient elasticsearch)
{
    var response = await elasticsearch.SearchAsync<CatalogItemIndex>(s => s
        .Index(CatalogItemIndex.IndexName)
        .From(0)
        .Size(10)
        .Query(q => q
            .Bool(b => b
                .Must(
                    mu => mu.Match(m => m.Field(f => f.Name).Query(qrName)),
                    mu => mu.Match(m => m.Field(f => f.Description).Query(qrDescription))
                )
            )
        )
    );

    if (response.IsValidResponse)
        return TypedResults.Ok(response.Documents);

    return TypedResults.NotFound();
}
