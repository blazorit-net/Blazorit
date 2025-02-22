using Blazorit.Core.Extensions;
using Microsoft.Extensions.Configuration;

namespace Blazorit.Core.Factories.EShop;

public static class IdentConnectionFactory
{
    public const string SCHEME_IDENT = "ident";

    private const string hostVariableName = "APP_POSTGRESQL_ESHOP_HOST";
    private const string portVariableName = "APP_POSTGRESQL_ESHOP_PORT";
    private const string databaseVariableName = "APP_POSTGRESQL_ESHOP_NAME";
    private const string userVariableName = "APP_POSTGRESQL_ESHOP_USER";
    private const string passwordVariableName = "APP_POSTGRESQL_ESHOP_PASSWORD";

    public static string Create(IConfiguration configuration)
    {
        ConnectionStringBuilder builder = new();

        builder.HasHost(configuration[hostVariableName] ?? throw new Exception($"Missing '{hostVariableName}' variable in Environment Configuration. Exception catched in {nameof(IdentConnectionFactory)}."));
        builder.HasPort(configuration[portVariableName] ?? throw new Exception($"Missing '{portVariableName}' variable in Environment Configuration. Exception catched in {nameof(IdentConnectionFactory)}."));
        builder.HasDatabase(configuration[databaseVariableName] ?? throw new Exception($"Missing '{databaseVariableName}' variable in Environment Configuration. Exception catched in {nameof(IdentConnectionFactory)}."));
        builder.HasUser(configuration[userVariableName] ?? throw new Exception($"Missing '{userVariableName}' variable in Environment Configuration. Exception catched in {nameof(IdentConnectionFactory)}."));
        builder.HasPassword(configuration[passwordVariableName] ?? throw new Exception($"Missing '{passwordVariableName}' variable in Environment Configuration. Exception catched in {nameof(IdentConnectionFactory)}."));

        builder.HasScheme(SCHEME_IDENT);

        return builder.Build();
    }
}