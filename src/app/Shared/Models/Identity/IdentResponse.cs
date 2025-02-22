
namespace Blazorit.Shared.Models.Identity
{
    public class IdentResponse<T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; } = false;
        public string Message { get; set; } = string.Empty;
    }
}
