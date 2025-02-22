using Blazorit.Infrastructure.DBStorages.EShop.EF;
using Microsoft.EntityFrameworkCore;

namespace Blazorit.Server;

/// <summary>
/// Databases initializer
/// </summary>
public class DbInitializer
{
    /// <summary>
    /// Initialize databases from migrations
    /// </summary>
    /// <param name="app"></param>
    public static async Task MigrateDatabaseAsync(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        
        // соблюдаем порядок миграций
        //// await MigrateDbContext(scope.ServiceProvider.GetService<PublicDbContext>()); public миграции, например, добавление extensions в PostgreSQL
        await MigrateDbContext(scope.ServiceProvider.GetService<IdentDbContext>());
        await MigrateDbContext(scope.ServiceProvider.GetService<DomDbContext>());
    }


    private static async Task MigrateDbContext(DbContext? dbContext)
    {
        ArgumentNullException.ThrowIfNull(dbContext);
        
        if (dbContext.Database.GetPendingMigrations().Any())
        {
            await dbContext.Database.MigrateAsync();
        }
    }
}