using Linode.Models.NetworkTransferPrices;

namespace Linode.Tests.TestHelpers.Models;

public static class NetworkTransferPricesModelHelper
{
    public const string DefaultPriceJsonResponse = """
                                                   {
                                                     "id": "network_transfer",
                                                     "label": "Network Transfer",
                                                     "price": {
                                                       "hourly": 0.005,
                                                       "monthly": null
                                                     },
                                                     "region_prices": [
                                                       {
                                                         "hourly": 0.015,
                                                         "id": "us-east",
                                                         "monthly": null
                                                       }
                                                     ],
                                                     "transfer": 0
                                                   }
                                                   """;

    public static readonly NetworkTransferPrice DefaultPrice = new()
    {
        Id = "network_transfer",
        Label = "Network Transfer",
        Price = new Price { Hourly = 0.005M, Monthly = null },
        RegionPrices =
        [
            new RegionPrice { Hourly = 0.015M, Id = "us-east", Monthly = null }
        ],
        Transfer = 0
    };
}
