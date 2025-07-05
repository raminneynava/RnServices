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