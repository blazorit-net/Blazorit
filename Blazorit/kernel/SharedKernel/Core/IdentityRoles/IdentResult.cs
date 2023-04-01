namespace Blazorit.SharedKernel.Core.IdentityRoles
{
    public class IdentResult<T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; } = false;
        public string Message { get; set; } = string.Empty;
    }
}
