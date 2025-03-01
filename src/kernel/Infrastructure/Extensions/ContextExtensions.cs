using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Blazorit.Infrastructure.Extensions;

public static class ContextExtensions
{
    public static void ConfigDatabase(this DbContextOptionsBuilder optionsBuilder, string connectionString, string? assemblyName = null)
    {
        optionsBuilder
            .UseNpgsql(connectionString, builder =>
            {
                var scheme = connectionString.GetScheme();
                if (scheme is not null)
                {
                    builder.MigrationsHistoryTable(HistoryRepository.DefaultTableName, scheme);
                }

                if (assemblyName is not null)
                {
                    builder.MigrationsAssembly(assemblyName);
                }
            })
            .UseSnakeCaseNamingConvention();
    }

    public static string? GetScheme(this string connectionString)
    {
        return connectionString.Split(';')
            .Where(x => x.StartsWith("Search Path", StringComparison.OrdinalIgnoreCase))
            .Select(x => x.Split('=').Last())
            .SingleOrDefault();
    }
}