
namespace Blazorit.Shared.Models.Universal {
    /// <summary>
    /// Response from Server to Client if Data can be Empty(Null) and it is a business logic
    /// </summary>
    /// <typeparam name="T"></typeparam>
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

        // [JsonInclude] if 'private set;'
        public T? Data { get; init; }
        public bool Ok { get; init; } = false;
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Method returns T.
        /// T should not have null.
        /// This method should be called when Ok is true
        /// </summary>
        /// <returns></returns>
        public T GetData()
        {
            return Data!;
        }
    }
}
