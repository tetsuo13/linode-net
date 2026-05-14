# Linode API Client

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

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
| Regions | |
| Resource Locking | |
| Support | |
| Tags | |
| Volumes | |
| VPCs | |

## Usage

Uses the Microsoft.Extensions.DependencyInjection library to handle all setup. Call an extension method to take care of it:

```csharp
var builder = Host.CreateApplicationBuilder(args);

// Get personal access token (PAT) from AWS Secrets Manager, Key Vault, or wherever...

builder.Services.AddLinodeApi(pat);
```

Let dependency injection take care of providing the instance variable by referencing `ILinodeClient` where you need it.

## Copyright and License

Copyright 2026 Andrei Nicholson

Licensed under the [MIT License](./LICENSE)

