using System.Text.Json.Serialization;
using Linode.Models.Internal;

namespace Linode.Models.NetworkTransferPrices.Internal;

internal sealed record NetworkTransferPriceResponse : IMapsTo<NetworkTransferPrice>
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("label")]
    public required string Label { get; init; }

    [JsonPropertyName("price")]
    public required PriceResponse Price { get; init; }

    [JsonPropertyName("region_prices")]
    public IReadOnlyList<RegionPriceResponse> RegionPrices { get; init; } = [];

    [JsonPropertyName("transfer")]
    public int Transfer { get; init; }

    public NetworkTransferPrice ToDomain() =>
        new()
        {
            Id = Id,
            Label = Label,
            Price = new Price
            {
                Hourly = Price.Hourly,
                Monthly = Price.Monthly
            },
            RegionPrices = RegionPrices.Select(x => new RegionPrice
            {
                Hourly = x.Hourly,
                Id = x.Id,
                Monthly = x.Monthly
            }).ToList(),
            Transfer = Transfer
        };
}

internal record PriceResponse
{
    [JsonPropertyName("hourly")]
    public decimal Hourly { get; init; }

    [JsonPropertyName("monthly")]
    public decimal? Monthly { get; init; }
}

internal sealed record RegionPriceResponse : PriceResponse
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }
}
