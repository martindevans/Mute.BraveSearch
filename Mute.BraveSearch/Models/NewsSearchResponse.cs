using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace Mute.BraveSearch.Models;

/// <summary>
/// The top-level news search response from the Brave News Search API.
/// </summary>
/// <param name="Type">The type of the response (always "news").</param>
/// <param name="Query">Information about the processed query.</param>
/// <param name="Results">The list of news search results.</param>
[UsedImplicitly]
public record NewsSearchResponse(
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("query")] Query Query,
    [property: JsonPropertyName("results")] IReadOnlyList<NewsResult> Results
);

/// <summary>
/// A single news search result.
/// </summary>
/// <param name="Title">The title of the news article.</param>
/// <param name="Url">The URL of the news article.</param>
/// <param name="Description">A description or snippet of the news article.</param>
/// <param name="Age">The age of the content, if known.</param>
/// <param name="PageAge">The age of the page, if known.</param>
/// <param name="MetaUrl">Structured information about the URL.</param>
/// <param name="Thumbnail">Thumbnail information, if available.</param>
/// <param name="ExtraSnippets">Additional snippets for the result, if requested.</param>
/// <param name="Profile">Profile of the news source.</param>
[UsedImplicitly]
public record NewsResult(
    [property: JsonPropertyName("title")] string Title,
    [property: JsonPropertyName("url")] string Url,

    [property: JsonPropertyName("description")] string? Description,
    [property: JsonPropertyName("age")] string? Age = null,
    [property: JsonPropertyName("page_age")] DateTime? PageAge = null,
    [property: JsonPropertyName("meta_url")] MetaUrl? MetaUrl = null,
    [property: JsonPropertyName("thumbnail")] Thumbnail? Thumbnail = null,
    [property: JsonPropertyName("extra_snippets")] IReadOnlyList<string>? ExtraSnippets = null,

    [property: JsonPropertyName("profile")] Profile? Profile = null
);

/// <summary>
/// News source profile
/// </summary>
/// <param name="Name"></param>
/// <param name="Url"></param>
/// <param name="LongName"></param>
/// <param name="Image"></param>
[UsedImplicitly]
public record Profile(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("url")] string Url,

    [property: JsonPropertyName("long_name")] string? LongName,
    [property: JsonPropertyName("img")] string? Image
);