
namespace Blazorit.Core.Services.Models.Identity {
    public class ServiceResult<T> {
        public T? Data { get; set; }
        public bool Success { get; set; } = false;
        public string Message { get; set; } = string.Empty;
    }
}
