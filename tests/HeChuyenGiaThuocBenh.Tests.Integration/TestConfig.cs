using Microsoft.Extensions.Configuration;

namespace HeChuyenGiaThuocBenh.Tests.Integration;

internal static class TestConfig
{
    public static string ConnectionString { get; } = new ConfigurationBuilder()
        .SetBasePath(AppContext.BaseDirectory)
        .AddJsonFile("appsettings.json", optional: false)
        .Build()
        .GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string not found in appsettings.json");
}
