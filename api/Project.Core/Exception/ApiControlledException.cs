namespace api
{
    public class ApiControlledException : Exception
    {
        public int StatusCode;
        public string Details;
        public ApiControlledException(string message, int statusCode = 400 , string details = "Wystąpił błąd") : base(message)
        {
            StatusCode = statusCode;
        }
        public ApiControlledException(string message, Exception innerException, int statusCode = 400, string details = "Wystąpił błąd") : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }
}

