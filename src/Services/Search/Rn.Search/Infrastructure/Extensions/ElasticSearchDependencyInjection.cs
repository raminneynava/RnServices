using Elastic.Clients.Elasticsearch;
using Elastic.Transport;

using Microsoft.Extensions.Options;

namespace Rn.Search.Infrastructure.Extensions
{
    public static class ElasticSearchDependencyInjection
    {
        public static void ElasticSearchConfigure(this IHostApplicationBuilder builder)
        {
            builder.Services.AddScoped(sp =>
            {
                var elasticSettings = sp.GetRequiredService<IOptions<AppSettings>>().Value.ElasticSearchOptions;
                var settings = new ElasticsearchClientSettings(new Uri(elasticSettings.Host));

                return new ElasticsearchClient(settings);
            });
        }
    }
}
