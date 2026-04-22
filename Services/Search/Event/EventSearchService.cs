using Elastic.Clients.Elasticsearch;

using Ticketing_backend.Documents.Event;

namespace Ticketing_backend.Services.Search.Event;

public class EventSearchService
{
    private readonly ElasticsearchClient _client;

    public EventSearchService(ElasticsearchClient client)
    {
        _client = client;
    }

    public async Task<List<EventSearchDocument>> SearchEventsAsync(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return new List<EventSearchDocument>();
        }

        var response = await _client.SearchAsync<EventSearchDocument>(s => s
            .Query(q => q
                .MultiMatch(m => m
                    .Query(query)                    
                    .Fields(new[] { "title^3", "venue^2", "organizerName^2", "description" })
                    .Fuzziness(new Fuzziness("AUTO"))
                )
            )
        );

        return response.IsValidResponse ?  response.Documents.ToList() : new List<EventSearchDocument>();
    }
}