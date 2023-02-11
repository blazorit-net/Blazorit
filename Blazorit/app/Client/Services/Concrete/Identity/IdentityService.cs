using Blazorit.Client.Services.Abstract.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using Blazorit.Shared.Models.Identity;
using Blazored.LocalStorage;
using System.Security.Claims;

namespace Blazorit.Client.Services.Concrete.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly HttpClient _http;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ILocalStorageService _localStorage;

        public IdentityService(HttpClient http, AuthenticationStateProvider authStateProvider, ILocalStorageService localStorageService)
        {
            _http = http;
            _authStateProvider = authStateProvider;
            _localStorage = localStorageService;
        }

        public async Task<bool> IsUserAuthenticated()
        {
            return (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity?.IsAuthenticated ?? false;
        }

        public async Task LoginAtClient(string? token)
        {
            await _localStorage.SetItemAsync("identityToken", token);
            await _authStateProvider.GetAuthenticationStateAsync();
        }

        public async Task LogoutAsync()
        {
            await _localStorage.RemoveItemAsync("identityToken");
            await _authStateProvider.GetAuthenticationStateAsync();
        }

        public async Task<Response<string>> LoginAtServer(UserLogin request)
        {
            var result = await _http.PostAsJsonAsync("api/identity/login", request);
            return await result.Content.ReadFromJsonAsync<Response<string>>() ?? new Response<string> { Success = false, Message = "Error response" };
        }

        public async Task<Response<bool>> ChangePassword(UserChangePassword request)
        {
            var result = await _http.PostAsJsonAsync("api/identity/change-password", request.Password);
            return await result.Content.ReadFromJsonAsync<Response<bool>>() ?? new Response<bool> { Success = false, Message = "Error response" };
        }

        public async Task<Response<int>> Register(UserRegister request)
        {
            var result = await _http.PostAsJsonAsync("api/identity/register", request);
            return await result.Content.ReadFromJsonAsync<Response<int>>() ?? new Response<int> { Success = false, Message = "Error response" };
        }

    }
}
