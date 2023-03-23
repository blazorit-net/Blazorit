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
            try 
            {
                return await client.GetFromJsonAsync<TValue>(requestUri, cancellationToken);                 
            } 
            catch (Exception ex) 
            {
#if DEBUG
                WriteErrorToConsole(ex, requestUri);
#endif
            }

            return default;
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
            }
            catch (Exception ex)
            {
#if DEBUG
                WriteErrorToConsole(ex, requestUri);
#endif
            }

            return default;
        }


        /// <summary>
        /// Extension post-method for requestUri without json param. Return TOut or default
        /// </summary>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="client"></param>
        /// <param name="requestUri"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<TOut?> PostAndReadAsJsonOrDefaultAsync<TOut>(this HttpClient client, [StringSyntax(StringSyntaxAttribute.Uri)] string? requestUri, CancellationToken cancellationToken = default)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsync(requestUri, null, cancellationToken);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return await response.Content.ReadFromJsonAsync<TOut>();
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                WriteErrorToConsole(ex, requestUri);
#endif
            }

            return default;
        }





        /// <summary>
        /// Extension method returns TValue or new TValue()
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="client"></param>
        /// <param name="requestUri"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<TValue> GetFromJsonOrNewAsync<TValue>(this HttpClient client, [StringSyntax(StringSyntaxAttribute.Uri)] string? requestUri, CancellationToken cancellationToken = default) where TValue : new()
        {
            try
            {
                var result = await client.GetFromJsonAsync<TValue>(requestUri, cancellationToken);
                return result ?? new TValue();
            }
            catch (Exception ex)
            {
#if DEBUG
                WriteErrorToConsole(ex, requestUri);
#endif
            }

            return new TValue();
        }


        /// <summary>
        /// Extension method for POST-request returns TOut or new TOut()
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="client"></param>
        /// <param name="requestUri"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<TOut> PostAndReadAsJsonOrNewAsync<TIn, TOut>(this HttpClient client, [StringSyntax(StringSyntaxAttribute.Uri)] string? requestUri, TIn value, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default) where TOut : new()
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(requestUri, value, options, cancellationToken);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadFromJsonAsync<TOut>();
                    return result ?? new TOut();
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                WriteErrorToConsole(ex, requestUri);
#endif
            }

            return new TOut();
        }


        /// <summary>
        /// Extension Post-method for requestUri without json param. Return TOut or new TOut()
        /// </summary>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="client"></param>
        /// <param name="requestUri"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<TOut> PostAndReadAsJsonOrNewAsync<TOut>(this HttpClient client, [StringSyntax(StringSyntaxAttribute.Uri)] string? requestUri, CancellationToken cancellationToken = default) where TOut : new()
        {
            try
            {
                HttpResponseMessage response = await client.PostAsync(requestUri, null, cancellationToken);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadFromJsonAsync<TOut>();
                    return result ?? new TOut();
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                WriteErrorToConsole(ex, requestUri);
#endif
            }

            return new TOut();
        }


        private static void WriteErrorToConsole(Exception ex, string? requestUri)
        {
            Console.WriteLine($"Blazorit: Request error: '{requestUri ?? string.Empty}'. default returned.");
            Console.WriteLine(ex.ToString());
            Console.WriteLine(ex.InnerException?.Message ?? string.Empty);
        }
    }
}
