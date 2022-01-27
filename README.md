# ZNetCS.AspNetCore.IPFiltering

[![NuGet](https://img.shields.io/nuget/v/ZNetCS.AspNetCore.IPFiltering.svg)](https://www.nuget.org/packages/ZNetCS.AspNetCore.IPFiltering)
[![Build](https://github.com/msmolka/ZNetCS.AspNetCore.IPFiltering/workflows/build/badge.svg)](https://github.com/msmolka/ZNetCS.AspNetCore.IPFiltering/actions)

A middleware that allows whitelist or blacklist incomming requests based on IP address. It can be configured using single IP address or ranges.
It supports single IP, IP range IPv4 and IPv6. There is also possible to ignore specific paths from IP filtering.

## Installing 

Install using the [ZNetCS.AspNetCore.IPFiltering NuGet package](https://www.nuget.org/packages/ZNetCS.AspNetCore.IPFiltering)

```
PM> Install-Package ZNetCS.AspNetCore.IPFiltering
```

## Usage 

When you install the package, it should be added to your `.csproj`. Alternatively, you can add it directly by adding:


```xml
<ItemGroup>
    <PackageReference Include="ZNetCS.AspNetCore.IPFiltering" Version="6.0.1" />
</ItemGroup>
```

### .NET 6
In order to use the IP filtering middleware, you must configure the services in the `Program.cs` file.

```c#
// Add services to the container.
builder.Services.AddIPFiltering(builder.Configuration.GetSection("IPFiltering"));
```
or
```c#
// Add services to the container.
builder.Services.AddIPFiltering(
        opts =>
        {
            opts.DefaultBlockLevel = DefaultBlockLevel.All;
            opts.HttpStatusCode = HttpStatusCode.NotFound;
            opts.Blacklist = new List<string> { "192.168.0.100-192.168.1.200" };
            opts.Whitelist = new List<string> { "192.168.0.10-192.168.10.20", "fe80::/10" };
            opts.IgnoredPaths = new List<string> { "get:/ignoreget", "*:/ignore" };
            opts.PathOptions = new List<PathOptions> { ... };
        });
```

then
```c#
// Configure IP filtering
app.UseIPFiltering();
```

### .NET 5 and Below

In order to use the IP filtering middleware, you must configure the services in the `ConfigureServices` and `Configure` call of `Startup`. Make
sure middleware is added just after logging to prevent any other middleware to run, so block is most effective: 


```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddIPFiltering(this.Configuration.GetSection("IPFiltering"));
}
```
or

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddIPFiltering(
        opts =>
        {
            opts.DefaultBlockLevel = DefaultBlockLevel.All;
            opts.HttpStatusCode = HttpStatusCode.NotFound;
            opts.Blacklist = new List<string> { "192.168.0.100-192.168.1.200" };
            opts.Whitelist = new List<string> { "192.168.0.10-192.168.10.20", "fe80::/10" };
            opts.IgnoredPaths = new List<string> { "get:/ignoreget", "*:/ignore" };
            opts.PathOptions = new List<PathOptions> { ... };
        });
}
```
```c#
public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
{   
    app.UseIPFiltering();

    // other middleware e.g. MVC etc
}
```

## File
Middleware can be configured in `appsettings.json` file. By adding following section and use following `ConfigureServices` method:

```json
{
    "IPFiltering": {
        "DefaultBlockLevel": "All",
        "HttpStatusCode": 404,
        "Whitelist": [ "192.168.0.10-192.168.10.20", "fe80::/10" ],
        "Blacklist": [ "192.168.0.100-192.168.1.200"],
        "IgnoredPaths": [ "GET:/ignoreget", "*:/ignore" ],
        "PathOptions": [
            {
                "Paths": [ "GET:/pathget", "*:/path" ],
                "DefaultBlockLevel": "None",
                "HttpStatusCode": 401,
                "Whitelist": [ "192.168.0.100-192.168.1.200" ],
                "Blacklist": [ "192.168.0.10-192.168.10.20", "fe80::/10" ]
            }, 
            {
                "Paths": [ "GET:/path2get", "*:/path2" ],
                "DefaultBlockLevel": "All",
                "HttpStatusCode": 401,
                "Whitelist": [ "192.168.0.10-192.168.10.20", "fe80::/10" ],
                "Blacklist": [ "192.168.0.100-192.168.1.200" ]
          }
        ]
    }
}
```

## Configuration
This middleware can be configured using following configuration options:

 * `DefaultBlockLevel` defines default action when IP address is not listed. Can be configured to `None` or `All`. Default value is `All`.
 * `HttpStatusCode` defines status code that is returned to client when IP address is forbidden. Default value is `404` (`Not Found`).
 * `Whitelist` defines list of IP address ranges that are allowed for request.
 * `Blacklist` defines list of IP address ranges that are forbidden for request.
 * `IgnoredPaths` defines list of paths with HTTP Verb to be ignored from IP filtering. `*` means all HTTP Verbs for given path will be ignored. Format `{VERB}:{PATH}` (no space after `:`). This configuration is case insensitive.
 * `PathOptions` defines list of paths with HTTP Verb to be processed with custom rules. `*` means all HTTP Verbs for given path will be ignored. Format `{VERB}:{PATH}` (no space after `:`). This configuration is case insensitive.

### IP Address Ranges
`Whitelist` and `Blacklist` can be defined as single IP address or IP address range. For parsing middleware is using extenal 
package: https://github.com/jsakamoto/ipaddressrange. Ranges can be defined in following formats:

 * `192.168.0.0/255.255.255.0`
 * `192.168.0.10-192.168.10.20`
 * `fe80::/10`

