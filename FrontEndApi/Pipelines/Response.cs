namespace OrderProcessingApi.Pipelines
{
    public class Response<T>
    {
        public bool Succeeded { get; set; }

        public T? Data { get; set; }

        public string? Message { get; set; }

        public static Response<T> Success(T data)
        {
            return new Response<T> { Succeeded = true, Data = data };
        }

        public static Response<T> Fail(string errorMessage)
        {
            return new Response<T> { Succeeded = false, Message = errorMessage };
        }
    }
}
