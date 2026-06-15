# Linode API Client

[![Continuous integration](https://github.com/tetsuo13/linode-net/actions/workflows/ci.yml/badge.svg)](https://github.com/tetsuo13/linode-net/actions/workflows/ci.yml)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![Linode](https://img.shields.io/nuget/v/Linode.svg)](https://www.nuget.org/packages/Linode/)

Linode API client is a library that provides an easy way to interact with the [Linode API](https://techdocs.akamai.com/linode-api/reference/api-summary).

Currently only supports authentication via a personal access token (PAT). See the [getting started with the Linode API doc](https://techdocs.akamai.com/linode-api/reference/get-started#authentication) for info on managing PATs.

## Supported Operations

| Operation | Status |
| --- | --- |
| Administration | |
| Beta Programs | |
| Databases | |
| Domains | Complete |
| Identity and Access | |
| Images | |
| Linode Instances | |
| Linode Kubernetes Engine | |
| Linode StackScripts | |
| Longview | |
| Maintenance | |
| Managed | |
| Marketplace | |
| Monitor | |
| Network Transfer Prices | |
| Networking | |
| NodeBalancers | |
| Object Storage | |
| Placement Groups | |
| Profile |  |
| Regions | Complete |
| Resource Locking | |
| Support | |
| Tags | Complete |
| Volumes | |
| VPCs | |

## Basic Usage

Uses the Microsoft.Extensions.DependencyInjection library to handle all setup. Call an extension method to take care of it:

```csharp
var builder = Host.CreateApplicationBuilder(args);

// Get personal access token (PAT) from AWS Secrets Manager, Key Vault, or wherever...

builder.Services.AddLinodeApi(pat);
```

Let dependency injection take care of providing the instance variable by referencing `ILinodeClient` where you need it.

```csharp
using Linode;

var domainsList = await _linodeClient.Domains.List(cancellationToken);

if (!domainsList.Successful || domainsList.Data is null)
{
    _logger.LogError("Error getting list of domains: {@Errors}", domainList.Errors);
    return;
}

foreach (var domain in domainsList.Data)
{
    // Do something with domain
}
```

All API calls return a [`Response`](./src/Linode/Models/Response.cs) object that indicates whether it was successful via the `Successful` property. If not, examine the `Errors` property for a collection of reasons why the call failed. If it was successful then the `Data` property will contain any data that was requested or returned. Not all API calls return data.

