using Mute.BraveSearch.Models;

namespace Mute.BraveSearch;

/// <summary>
/// A request for the Brave Search API.
/// </summary>
/// <param name="Query">The search query (max 400 characters, 50 words).</param>
/// <param name="Country">2-character country code (default: "US").</param>
/// <param name="SearchLang">Language code for results (default: "en").</param>
/// <param name="Count">Number of results, max 20 (default: 20).</param>
/// <param name="Offset">Number of results to skip for pagination (max 9).</param>
/// <param name="SafeSearch">Safe search setting (off, moderate, or strict).</param>
/// <param name="Freshness">Filter by age (e.g., pd, pw, or a date range).</param>
/// <param name="ResultFilter">The flags for result types to include (e.g., web, news, videos).</param>
/// <param name="Spellcheck">Whether to enable spell check (default: true).</param>
/// <param name="Goggles">URL or definition for custom re-ranking.</param>
/// <param name="ExtraSnippets">Whether to include extra snippets in results.</param>
public record SearchRequest(
    string Query,
    string? Country = null,
    string? SearchLang = null,
    int? Count = null,
    int? Offset = null,
    SafeSearch? SafeSearch = null,
    string? Freshness = null,
    ResultType? ResultFilter = null,
    bool? Spellcheck = null,
    string? Goggles = null,
    bool? ExtraSnippets = null
);

/// <summary>
/// A client for the Brave Search API.
/// </summary>
public interface IBraveSearchClient
{
    /// <summary>
    /// Performs a search with the specified request parameters.
    /// </summary>
    Task<SearchResponse> SearchAsync(SearchRequest request, CancellationToken ct = default);

    /// <summary>
    /// Performs a simple search with only a query string.
    /// </summary>
    Task<SearchResponse> SearchAsync(string query, CancellationToken ct = default);
}
