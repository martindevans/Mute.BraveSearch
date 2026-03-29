using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Mute.BraveSearch.Models;

/// <summary>
/// The type of result returned by the Brave Search API.
/// </summary>
[Flags]
[JsonConverter(typeof(JsonStringEnumConverter<ResultType>))]
public enum ResultType
{
    /// <summary>
    /// No result type specified.
    /// </summary>
    None = 0,

    /// <summary>
    /// Top-level search response.
    /// </summary>
    [JsonStringEnumMemberName("search")] 
    Search = 1 << 0,

    /// <summary>
    /// Web search results.
    /// </summary>
    [JsonStringEnumMemberName("web")] 
    Web = 1 << 1,

    /// <summary>
    /// Forum and community discussions.
    /// </summary>
    [JsonStringEnumMemberName("discussions")] 
    Discussions = 1 << 2,

    /// <summary>
    /// Frequently asked questions.
    /// </summary>
    [JsonStringEnumMemberName("faq")] 
    Faq = 1 << 3,

    /// <summary>
    /// Aggregated entity information.
    /// </summary>
    [JsonStringEnumMemberName("infobox")] 
    Infobox = 1 << 4,

    /// <summary>
    /// News results.
    /// </summary>
    [JsonStringEnumMemberName("news")] 
    News = 1 << 5,

    /// <summary>
    /// Video results.
    /// </summary>
    [JsonStringEnumMemberName("videos")] 
    Videos = 1 << 6,

    /// <summary>
    /// Local search results.
    /// </summary>
    [JsonStringEnumMemberName("locations")] 
    Locations = 1 << 7,

    /// <summary>
    /// Recommended ranking order for all result types.
    /// </summary>
    [JsonStringEnumMemberName("mixed")] 
    Mixed = 1 << 8
}

internal static class ResultTypeExtensions
{
    public static string ToEnumMemberList(this ResultType value)
    {
        return JsonSerializer
            .Serialize(value)
            .ToLowerInvariant()
            .Replace(", ", ",")
            .Replace("\"", "");
    }
}