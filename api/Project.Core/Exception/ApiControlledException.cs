namespace api
{
    public class ApiControlledException : Exception
    {
        public int StatusCode;
        public ApiControlledException(string message) : base(message){}
        public ApiControlledException(string message, Exception innerException) : base(message, innerException){}
        public ApiControlledException(string message, Exception innerException, int statusCode) : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }
}