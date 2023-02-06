using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Json;

namespace Blazorit.Client.Support.Helpers {
    public static class HttpClientHalper {
        public static async Task<TValue?> GetFromJsonOrDefaultAsync<TValue>(this HttpClient client, [StringSyntax(StringSyntaxAttribute.Uri)] string? requestUri, CancellationToken cancellationToken = default) 
        {
            try {
                return await client.GetFromJsonAsync<TValue>(requestUri, cancellationToken);                 
            } catch {
                //////await Console.Error.WriteLineAsync($"Request error: '{requestUri ?? string.Empty}'. default returned.");         
                return default(TValue);
            }
        }
    }
}
