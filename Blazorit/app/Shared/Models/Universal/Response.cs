
namespace Blazorit.Shared.Models.Universal {
    public class Response<T> {

        public Response() { }

        public Response(string message)
        {
            Message = message;
        }

        public Response(T? data, string message)
        {
            Data = data;
            Message = message;

            if (data != null)
            {
                Ok = true;
            }
        }

        public T? Data { get; set; }
        public bool Ok { get; set; } = false;
        public string Message { get; set; } = string.Empty;
    }
}
