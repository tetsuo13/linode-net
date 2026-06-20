namespace Linode.Models.NetworkTransferPrices;

/// <summary>
/// Network transfer prices, including any region-specific rates.
/// </summary>
public record NetworkTransferPrice
{
    /// <summary>
    /// The ID representing the network transfer price.
    /// </summary>
    public required string Id { get; init; }

    /// <summary>
    /// The network transfer price label is for display purposes only.
    /// </summary>
    public required string Label { get; init; }

    /// <summary>
    /// The default cost of this network transfer. Prices are in US dollars,
    /// broken down into hourly and monthly charges.
    /// <para/>
    /// Certain regions have different prices from the default. For
    /// region-specific prices, see <see cref="RegionPrices"/>.
    /// </summary>
    public required Price Price { get; init; }

    /// <summary>
    /// The default cost of this network transfer in a region.
    /// </summary>
    public IReadOnlyList<RegionPrice> RegionPrices { get; init; } = [];

    /// <summary>
    /// The monthly outbound transfer amount, in MB.
    /// </summary>
    public int Transfer { get; init; }
}

/// <summary>
/// The default cost.
/// </summary>
public record Price
{
    /// <summary>
    /// Cost (in US dollars) per hour.
    /// </summary>
    public decimal Hourly { get; init; }

    /// <summary>
    /// Cost per month, in US dollars.
    /// </summary>
    public decimal? Monthly { get; init; }
}

/// <summary>
/// The default cost per region.
/// </summary>
public record RegionPrice : Price
{
    /// <summary>
    /// The Region ID for these prices.
    /// </summary>
    public required string Id { get; init; }
}
