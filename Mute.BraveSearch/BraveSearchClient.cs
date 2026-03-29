using System.Net.Http.Json;
using System.Text.Json;
using System.Web;
using Mute.BraveSearch.Models;

namespace Mute.BraveSearch;

/// <inheritdoc cref="IBraveSearchClient" />
public class BraveSearchClient
    : IBraveSearchClient
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    /// <summary>
    /// Construct a new <see cref="BraveSearchClient"/>
    /// </summary>
    /// <param name="httpClient"></param>
    /// <param name="apiKey"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public BraveSearchClient(HttpClient httpClient, string apiKey)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));

        if (_httpClient.BaseAddress == null)
        {
            _httpClient.BaseAddress = new Uri("https://api.search.brave.com/res/v1/");
        }
    }

    /// <inheritdoc />
    public async Task<SearchResponse> SearchAsync(string query, CancellationToken ct = default)
    {
        return await SearchAsync(new SearchRequest(query), ct);
    }

    /// <inheritdoc />
    public async Task<SearchResponse> SearchAsync(SearchRequest request, CancellationToken ct = default)
    {
        var uri = BuildUri(request);
        
        using var httpRequest = new HttpRequestMessage(HttpMethod.Get, uri);
        httpRequest.Headers.Add("Accept", "application/json");
        httpRequest.Headers.Add("x-subscription-token", _apiKey);

        using var response = await _httpClient.SendAsync(httpRequest, ct);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<SearchResponse>(cancellationToken: ct);
        return result ?? throw new JsonException("Failed to deserialize search response.");
    }

    private static string BuildUri(SearchRequest request)
    {
        var query = HttpUtility.ParseQueryString(string.Empty);
        query["q"] = request.Query;

        if (request.Country != null)
            query["country"] = request.Country;
        if (request.SearchLang != null)
            query["search_lang"] = request.SearchLang;
        if (request.Count != null) query["count"] = request.Count.Value.ToString();
        if (request.Offset != null) query["offset"] = request.Offset.Value.ToString();
        if (request.SafeSearch != null) query["safesearch"] = request.SafeSearch.Value.ToString().ToLowerInvariant();
        if (request.Freshness != null) query["freshness"] = request.Freshness;

        if (request.ResultFilter != null && request.ResultFilter != ResultType.None)
            query["result_filter"] = request.ResultFilter.Value.ToString().Replace(" ", "").ToLowerInvariant();

        if (request.Spellcheck != null)
            query["spellcheck"] = request.Spellcheck.Value.ToString().ToLowerInvariant();
        if (request.Goggles != null)
            query["goggles"] = request.Goggles;
        if (request.ExtraSnippets != null)
            query["extra_snippets"] = request.ExtraSnippets.Value.ToString().ToLowerInvariant();

        return $"web/search?{query}";
    }
}
