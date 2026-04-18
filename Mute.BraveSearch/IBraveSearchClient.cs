using Mute.BraveSearch.Models;

namespace Mute.BraveSearch;

/// <summary>
/// Base class for all search request types
/// </summary>
public abstract record BaseSearchRequest
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="query">The search query (max 400 characters, 50 words)</param>
    protected BaseSearchRequest(string query)
    {
        Query = query;
    }

    /// <summary>
    /// The search query (max 400 characters, 50 words)
    /// </summary>
    public string Query { get; set; }

    /// <summary>
    /// 2-character country code (default: "US")
    /// </summary>
    public string? Country { get; set; }

    /// <summary>
    /// Language code for results (default: "en")
    /// </summary>
    public string? SearchLang { get; set; }

    /// <summary>
    /// Number of results, max 20 (default: 20)
    /// </summary>
    public int? Count { get; set; }

    /// <summary>
    /// Number of results to skip for pagination (max 9)
    /// </summary>
    public int? Offset { get; set; }

    /// <summary>
    /// Safe search setting
    /// </summary>
    public SafeSearch? SafeSearch { get; set; }

    /// <summary>
    /// Filter by age (e.g., pd, pw, or a date range)
    /// </summary>
    public SearchFreshness? Freshness { get; set; }

    /// <summary>
    /// Whether to enable spell check
    /// </summary>
    public bool? Spellcheck { get; set; }

    /// <summary>
    /// URL or definition for custom re-ranking
    /// </summary>
    public string? Goggles { get; set; }

    /// <summary>
    /// Whether to include extra snippets in results
    /// </summary>
    public bool? ExtraSnippets { get; set; }
}

/// <summary>
/// Specifies the "freshness" of search results
/// </summary>
public sealed record SearchFreshness
{
    private static readonly SearchFreshness _pd = new("pd");
    private static readonly SearchFreshness _pw = new("pw");
    private static readonly SearchFreshness _pm = new("pm");
    private static readonly SearchFreshness _py = new("py");
    
    private readonly string _value;

    private SearchFreshness(string value)
    {
        _value = value;
    }
    
    /// <summary>
    /// Fetch results from last 24 hours
    /// </summary>
    /// <returns></returns>
    public static SearchFreshness Day() => _pd;

    /// <summary>
    /// Fetch results from last 7 days
    /// </summary>
    /// <returns></returns>
    public static SearchFreshness Week() => _pw;

    /// <summary>
    /// Fetch results from last month
    /// </summary>
    /// <returns></returns>
    public static SearchFreshness Month() => _pm;

    /// <summary>
    /// Fetch results from last year
    /// </summary>
    /// <returns></returns>
    public static SearchFreshness Year() => _py;

    /// <summary>
    /// Fetch results from a specific date range
    /// </summary>
    /// <returns></returns>
    public static SearchFreshness Range(DateOnly start, DateOnly end)
    {
        // Ensure order is valid
        if (end < start)
            (start, end) = (end, start);
        
        // 2022-04-01to2022-07-30
        return new SearchFreshness($"{start:O}to{end:O}");
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return _value;
    }
}

/// <summary>
/// A request for the Brave Search API.
/// </summary>
public record SearchRequest
    : BaseSearchRequest
{
    /// <summary>
    /// A request for the Brave Search API.
    /// </summary>
    public SearchRequest(string query, ResultType? filter = null)
        : base(query)
    {
        ResultFilter = filter;
    }

    /// <summary>
    /// The flags for result types to include
    /// </summary>
    public ResultType? ResultFilter { get; set; }
}

/// <summary>
/// A request for the Brave News Search API.
/// </summary>
public record NewsSearchRequest
    : BaseSearchRequest
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="query"></param>
    public NewsSearchRequest(string query)
        : base(query)
    {
        
    }
}

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

    /// <summary>
    /// Performs a news search with the specified request parameters.
    /// </summary>
    Task<NewsSearchResponse> NewsSearchAsync(NewsSearchRequest request, CancellationToken ct = default);

    /// <summary>
    /// Performs a simple news search with only a query string.
    /// </summary>
    Task<NewsSearchResponse> NewsSearchAsync(string query, CancellationToken ct = default);
}
