using System.Text;

namespace Blazorit.Core.Extensions;

public sealed class ConnectionStringBuilder
{
    private readonly StringBuilder _builder = new();

    public ConnectionStringBuilder HasHost(string host)
    {
        _builder.Append($"Host={host};");
        return this;
    }
    
    public ConnectionStringBuilder HasPort(string port)
    {
        _builder.Append($"Port={port};");
        return this;
    }
    
    public ConnectionStringBuilder HasDatabase(string database)
    {
        _builder.Append($"Database={database};");
        return this;
    }
    
    public ConnectionStringBuilder HasUser(string username)
    {
        _builder.Append($"UserId={username};");
        return this;
    }
    
    public ConnectionStringBuilder HasPassword(string password)
    {
        _builder.Append($"Password={password};");
        return this;
    }
    
    public ConnectionStringBuilder HasScheme(string scheme)
    {
        _builder.Append($"Search Path={scheme};");
        return this;
    }
    
    public string Build() => _builder.ToString();
}