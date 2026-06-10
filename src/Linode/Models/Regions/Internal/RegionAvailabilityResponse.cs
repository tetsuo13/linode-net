using System.Text.Json.Serialization;
using Linode.Models.Internal;

namespace Linode.Models.Regions.Internal;

internal record RegionAvailabilityResponse : IMapsTo<RegionAvailability>
{
    [JsonPropertyName("available")]
    public bool Available { get; init; }

    [JsonPropertyName("plan")]
    public required string Plan { get; init; }

    [JsonPropertyName("region")]
    public required string Region { get; init; }

    public RegionAvailability ToDomain() =>
        new()
        {
            Available = Available,
            Plan = Plan,
            Region = Region
        };
}
