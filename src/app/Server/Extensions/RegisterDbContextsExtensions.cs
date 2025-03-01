using Blazorit.Core.Factories.EShop;
using Blazorit.Infrastructure.DBStorages.EShop.EF;
using Blazorit.Infrastructure.Extensions;

namespace Blazorit.Server.Extensions;

public static class RegisterDbContextsExtensions
{
    public static void AddDbContextsFactories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextFactory<IdentDbContext>(options => options.ConfigDatabase(IdentConnectionFactory.Create(configuration)));
        services.AddDbContextFactory<DomDbContext>(options => options.ConfigDatabase(DomConnectionFactory.Create(configuration)));
    }
    
    /*
    public static void AddDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IdentDbContext>(options => options.ConfigDatabase(IdentConnectionFactory.Create(configuration)));
        services.AddDbContext<DomDbContext>(options => options.ConfigDatabase(DomConnectionFactory.Create(configuration)));
    }
    */
}