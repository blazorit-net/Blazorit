using Blazorit.Client.Services.Abstract.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using Blazorit.Shared.Models.Identity;
using Blazored.LocalStorage;
using System.Security.Claims;
using Blazorit.Shared.Routes.WebAPI.Identity;
using Blazorit.Client.Models.Identity;

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
            await _localStorage.SetItemAsync(Constants.STORAGE_TOKEN_NAME, token);
            await _authStateProvider.GetAuthenticationStateAsync();
        }

        public async Task LogoutAsync()
        {
            await _localStorage.RemoveItemAsync(Constants.STORAGE_TOKEN_NAME);
            await _authStateProvider.GetAuthenticationStateAsync();
        }

        public async Task<IdentResponse<string>> LoginAtServer(UserLogin request)
        {
            var result = await _http.PostAsJsonAsync($"{IdentApi.CONTROLLER}/{IdentApi.LOGIN}", request);
            return await result.Content.ReadFromJsonAsync<IdentResponse<string>>() ?? new IdentResponse<string> { Success = false, Message = "Error response" };
        }

        public async Task<IdentResponse<bool>> ChangePassword(UserChangePassword request)
        {
            var result = await _http.PostAsJsonAsync($"{IdentApi.CONTROLLER}/{IdentApi.CHANGE_PASSWORD}", request.Password);
            return await result.Content.ReadFromJsonAsync<IdentResponse<bool>>() ?? new IdentResponse<bool> { Success = false, Message = "Error response" };
        }

        public async Task<IdentResponse<int>> Register(UserRegister request)
        {
            var result = await _http.PostAsJsonAsync($"{IdentApi.CONTROLLER}/{IdentApi.REGISTER}", request);
            return await result.Content.ReadFromJsonAsync<IdentResponse<int>>() ?? new IdentResponse<int> { Success = false, Message = "Error response" };
        }

    }
}
