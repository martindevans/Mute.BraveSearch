using System.Text.Json.Serialization;

namespace Mute.BraveSearch.Models;

/// <summary>
/// Safe search setting for the Brave Search API.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<SafeSearch>))]
public enum SafeSearch
{
    /// <summary>
    /// Safe search is off.
    /// </summary>
    [JsonStringEnumMemberName("off")] Off,

    /// <summary>
    /// Safe search is moderate.
    /// </summary>
    [JsonStringEnumMemberName("moderate")] Moderate,

    /// <summary>
    /// Safe search is strict.
    /// </summary>
    [JsonStringEnumMemberName("strict")] Strict
}
