using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace Mute.BraveSearch.Models;

/// <summary>
/// The top-level search response from the Brave Search API.
/// </summary>
/// <param name="Type">The type of the response (always "search").</param>
/// <param name="Query">Information about the processed query.</param>
/// <param name="Web">Web search results, if requested.</param>
/// <param name="Mixed">The recommended ranking order for all result types.</param>
/// <param name="Discussions">Forum/community results, if available.</param>
/// <param name="Faq">Frequently asked questions, if available.</param>
/// <param name="Infobox">Aggregated entity information, if available.</param>
/// <param name="News">News results, if available.</param>
/// <param name="Videos">Video results, if available.</param>
/// <param name="Locations">Local search results, if available.</param>
public record SearchResponse(
    [property: JsonPropertyName("type")] ResultType Type,
    [property: JsonPropertyName("query")] Query Query,
    [property: JsonPropertyName("web")] Web? Web = null,
    [property: JsonPropertyName("mixed")] Mixed? Mixed = null,
    [property: JsonPropertyName("discussions")] Discussions? Discussions = null,
    [property: JsonPropertyName("faq")] Faq? Faq = null,
    [property: JsonPropertyName("infobox")] Infobox? Infobox = null,
    [property: JsonPropertyName("news")] News? News = null,
    [property: JsonPropertyName("videos")] Videos? Videos = null,
    [property: JsonPropertyName("locations")] Locations? Locations = null
);

/// <summary>
/// Information about the search query as processed by the API.
/// </summary>
/// <param name="Original">The original search query.</param>
/// <param name="ShowStrictWarning">Whether a strict warning should be shown.</param>
/// <param name="IsNavigational">Whether the query is identified as navigational.</param>
/// <param name="IsNewsBreaking">Whether the query relates to breaking news.</param>
/// <param name="AskForLocation">Whether the API recommends asking the user for their location.</param>
/// <param name="Language">The language code used for the search.</param>
/// <param name="Country">The country code used for the search.</param>
/// <param name="BadResults">Whether the results are considered low quality.</param>
/// <param name="ShouldFallback">Whether a fallback to another search engine is recommended.</param>
/// <param name="Latitude">The latitude used for the search, if provided.</param>
/// <param name="Longitude">The longitude used for the search, if provided.</param>
/// <param name="Altered">The altered query used for the search (e.g., after spell correction).</param>
/// <param name="SpellcheckOff">Whether spellcheck was disabled for this query.</param>
[UsedImplicitly]
public record Query(
    [property: JsonPropertyName("original")] string Original,
    [property: JsonPropertyName("show_strict_warning")] bool ShowStrictWarning,
    [property: JsonPropertyName("is_navigational")] bool IsNavigational,
    [property: JsonPropertyName("is_news_breaking")] bool IsNewsBreaking,
    [property: JsonPropertyName("ask_for_location")] bool AskForLocation,
    [property: JsonPropertyName("language")] string Language,
    [property: JsonPropertyName("country")] string Country,
    [property: JsonPropertyName("bad_results")] bool BadResults,
    [property: JsonPropertyName("should_fallback")] bool ShouldFallback,
    [property: JsonPropertyName("lat")] string? Latitude = null,
    [property: JsonPropertyName("long")] string? Longitude = null,
    [property: JsonPropertyName("altered")] string? Altered = null,
    [property: JsonPropertyName("spellcheck_off")] bool? SpellcheckOff = null
);

/// <summary>
/// A collection of web search results.
/// </summary>
/// <param name="Type">The result type (always "web").</param>
/// <param name="Results">The list of web search results.</param>
[UsedImplicitly]
public record Web(
    [property: JsonPropertyName("type")] ResultType Type,
    [property: JsonPropertyName("results")] IReadOnlyList<WebResult> Results
);

/// <summary>
/// A single web search result.
/// </summary>
/// <param name="Title">The title of the web page.</param>
/// <param name="Url">The URL of the web page.</param>
/// <param name="Description">A description or snippet of the web page.</param>
/// <param name="Language">The language of the web page.</param>
/// <param name="FamilyFriendly">Whether the content is considered family-friendly.</param>
/// <param name="MetaUrl">Structured information about the URL.</param>
/// <param name="Age">The age of the content, if known.</param>
/// <param name="PageAge">The age of the page, if known.</param>
/// <param name="Thumbnail">Thumbnail information, if available.</param>
/// <param name="ExtraSnippets">Additional snippets for the result, if requested.</param>
[UsedImplicitly]
public record WebResult(
    [property: JsonPropertyName("title")] string Title,
    [property: JsonPropertyName("url")] string Url,
    [property: JsonPropertyName("description")] string Description,
    [property: JsonPropertyName("language")] string Language,
    [property: JsonPropertyName("family_friendly")] bool FamilyFriendly,
    [property: JsonPropertyName("meta_url")] MetaUrl MetaUrl,
    [property: JsonPropertyName("age")] string? Age = null,
    [property: JsonPropertyName("page_age")] string? PageAge = null,
    [property: JsonPropertyName("thumbnail")] Thumbnail? Thumbnail = null,
    [property: JsonPropertyName("extra_snippets")] IReadOnlyList<string>? ExtraSnippets = null
);

/// <summary>
/// Structured information about a URL.
/// </summary>
/// <param name="Scheme">The protocol scheme (e.g., "https").</param>
/// <param name="Netloc">The network location (e.g., "search.brave.com").</param>
/// <param name="Hostname">The hostname.</param>
/// <param name="Favicon">The URL to the favicon.</param>
/// <param name="Path">The URL path.</param>
[UsedImplicitly]
public record MetaUrl(
    [property: JsonPropertyName("scheme")] string Scheme,
    [property: JsonPropertyName("netloc")] string Netloc,
    [property: JsonPropertyName("hostname")] string Hostname,
    [property: JsonPropertyName("favicon")] string Favicon,
    [property: JsonPropertyName("path")] string Path
);

/// <summary>
/// Information about a result's thumbnail.
/// </summary>
/// <param name="Source">The URL of the thumbnail image.</param>
/// <param name="Original">The original image URL, if available.</param>
/// <param name="Logo">Whether the image is a logo.</param>
[UsedImplicitly]
public record Thumbnail(
    [property: JsonPropertyName("src")] string Source,
    [property: JsonPropertyName("original")] string? Original = null,
    [property: JsonPropertyName("logo")] bool? Logo = null
);

/// <summary>
/// The recommended ranking order for different result types.
/// </summary>
/// <param name="Type">The result type (always "mixed").</param>
/// <param name="Main">Results for the main results column.</param>
/// <param name="Top">Results for the top of the page.</param>
/// <param name="Side">Results for the sidebar.</param>
[UsedImplicitly]
public record Mixed(
    [property: JsonPropertyName("type")] ResultType Type,
    [property: JsonPropertyName("main")] IReadOnlyList<ResultReference> Main,
    [property: JsonPropertyName("top")] IReadOnlyList<ResultReference>? Top = null,
    [property: JsonPropertyName("side")] IReadOnlyList<ResultReference>? Side = null
);

/// <summary>
/// A reference to a result in another collection.
/// </summary>
/// <param name="Type">The type of the referenced result.</param>
/// <param name="Index">The index of the result within its collection.</param>
[UsedImplicitly]
public record ResultReference(
    [property: JsonPropertyName("type")] ResultType Type,
    [property: JsonPropertyName("index")] int Index
);

/// <summary>Forum and community discussion results.</summary>
/// <param name="Type">The result type.</param>
[UsedImplicitly]
public record Discussions([property: JsonPropertyName("type")] ResultType Type);

/// <summary>Frequently asked questions results.</summary>
/// <param name="Type">The result type.</param>
[UsedImplicitly]
public record Faq([property: JsonPropertyName("type")] ResultType Type);

/// <summary>Aggregated entity information results.</summary>
/// <param name="Type">The result type.</param>
[UsedImplicitly]
public record Infobox([property: JsonPropertyName("type")] ResultType Type);

/// <summary>News search results.</summary>
/// <param name="Type">The result type.</param>
[UsedImplicitly]
public record News([property: JsonPropertyName("type")] ResultType Type);

/// <summary>Video search results.</summary>
/// <param name="Type">The result type.</param>
[UsedImplicitly]
public record Videos([property: JsonPropertyName("type")] ResultType Type);

/// <summary>Local search results.</summary>
/// <param name="Type">The result type.</param>
[UsedImplicitly]
public record Locations([property: JsonPropertyName("type")] ResultType Type);
