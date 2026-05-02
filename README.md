# Linode API Client

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

Linode API client is a library that provides an easy way to interact with the [Linode API](https://techdocs.akamai.com/linode-api/reference/api-summary).

## Usage Examples

Uses the Microsoft.Extensions.DependencyInjection library to handle all setup. Call an extension method to take care of it:

```csharp
var builder = Host.CreateApplicationBuilder(args);

// Get personal access token from AWS Secrets Manager, Key Vault, or wherever...

builder.Services.AddLinodeApi(pat);
```

Let dependency injection take care of providing the instance variable by referencing `ILinodeClient` where you need it.

## Copyright and License

Copyright 2026 Andrei Nicholson

Licensed under the [MIT License](./LICENSE)

