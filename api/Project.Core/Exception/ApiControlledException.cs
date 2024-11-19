namespace api
{
    public class ApiControlledException : Exception
    {
        public int StatusCode;
        public string Details;
        public ApiControlledException(string message, int statusCode = 400 ,string details = null) : base(message)
        {
            Details = details ?? "An error occurred";
            StatusCode = statusCode;
        }
        public ApiControlledException(string message, Exception innerException, int statusCode = 400, string details = null) : base(message, innerException)
        {
            Details = details ?? "An error occurred";
            StatusCode = statusCode;
        }
    }
}