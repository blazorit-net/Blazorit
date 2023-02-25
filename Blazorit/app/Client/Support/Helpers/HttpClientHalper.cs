using Blazorit.Shared.Routes.WebAPI.ECommerce.Domain;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Carts;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace Blazorit.Client.Support.Helpers {
    public static class HttpClientHalper {
        /// <summary>
        /// Extension method returns TValue or default default(TValue)
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="client"></param>
        /// <param name="requestUri"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<TValue?> GetFromJsonOrDefaultAsync<TValue>(this HttpClient client, [StringSyntax(StringSyntaxAttribute.Uri)] string? requestUri, CancellationToken cancellationToken = default) 
        {
            try {
                return await client.GetFromJsonAsync<TValue>(requestUri, cancellationToken);                 
            } catch (Exception ex) {
#if DEBUG
                Console.WriteLine($"Blazorit: Request error: '{requestUri ?? string.Empty}'. default returned.");
                Console.WriteLine(ex.ToString());
                Console.WriteLine(ex.InnerException != null ? ex.InnerException.Message: string.Empty);
#endif
                               
            }

            return default(TValue);
        }


        /// <summary>
        /// Extension method for POST-request returns TOut or default default(TOut)
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="client"></param>
        /// <param name="requestUri"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<TOut?> PostAndReadAsJsonOrDefaultAsync<TIn, TOut>(this HttpClient client, [StringSyntax(StringSyntaxAttribute.Uri)] string? requestUri, TIn value, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(requestUri, value, options, cancellationToken);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return await response.Content.ReadFromJsonAsync<TOut>();
                }
                ////else
                ////{                    
                    
                ////    //Console.WriteLine($"Blazorit: Request error: Unauthorized at Server. Request: '{requestUri ?? string.Empty}'. default returned.");
                ////}
            }
            catch
            {
                //Console.WriteLine($"Blazorit: Request error: '{requestUri ?? string.Empty}'. default returned.");
            }

            return default(TOut);
        }


        public static async Task<TOut?> PostAndReadAsJsonOrDefaultAsync<TOut>(this HttpClient client, [StringSyntax(StringSyntaxAttribute.Uri)] string? requestUri, CancellationToken cancellationToken = default)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsync(requestUri, null, cancellationToken);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return await response.Content.ReadFromJsonAsync<TOut>();
                }
                ////else
                ////{                    

                ////    //Console.WriteLine($"Blazorit: Request error: Unauthorized at Server. Request: '{requestUri ?? string.Empty}'. default returned.");
                ////}
            }
            catch
            {
                //Console.WriteLine($"Blazorit: Request error: '{requestUri ?? string.Empty}'. default returned.");
            }

            return default(TOut);
        }

    }
}
