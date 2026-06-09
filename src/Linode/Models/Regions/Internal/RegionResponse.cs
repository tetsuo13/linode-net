using System.Text.Json.Serialization;
using Linode.Models.Internal;

namespace Linode.Models.Regions.Internal;

internal sealed class RegionResponse : IMapsTo<Region>
{
    [JsonPropertyName("capabilities")]
    public List<string> Capabilities { get; set; } = [];

    [JsonPropertyName("country")]
    public required string Country { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("label")]
    public required string Label { get; set; }

    [JsonPropertyName("monitors")]
    public required Monitors Monitors { get; set; }

    [JsonPropertyName("placement_group_limits")]
    public required PlacementGroupLimitsResponse PlacementGroupLimits { get; set; }

    [JsonPropertyName("resolvers")]
    public required Resolvers Resolvers { get; set; }

    [JsonPropertyName("site_type")]
    public SiteType SiteType { get; set; }

    [JsonPropertyName("status")]
    public RegionStatus Status { get; set; }

    public Region ToDomain() =>
        new()
        {
            Id = Id,
            Capabilities = Capabilities,
            Country = Country,
            Label = Label,
            Monitors = Monitors,
            PlacementGroupLimits = new PlacementGroupLimits
            {
                MaximumLinodesPerFlexiblePage = PlacementGroupLimits.MaximumLinodesPerFlexiblePage,
                MaximumLinodesPerPage = PlacementGroupLimits.MaximumLinodesPerPage,
                MaximumPagesPerCustomer = PlacementGroupLimits.MaximumPagesPerCustomer
            },
            Resolvers = Resolvers,
            SiteType = SiteType,
            Status = Status
        };
}

internal sealed record PlacementGroupLimitsResponse
{
    [JsonPropertyName("maximum_linodes_per_flexible_pg")]
    public int? MaximumLinodesPerFlexiblePage { get; set; }

    [JsonPropertyName("maximum_linodes_per_pg")]
    public int? MaximumLinodesPerPage { get; set; }

    [JsonPropertyName("maximum_pgs_per_customer")]
    public int? MaximumPagesPerCustomer { get; set; }
}
