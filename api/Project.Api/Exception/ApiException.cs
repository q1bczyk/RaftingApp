namespace Project.Core.Exceptions
{
    public class ApiException : Exception
    {
        public int StatusCode { get; set; }
        public string Details { get; set; }
        public ApiException(int statusCode, string message, string details) : base(message)
        {
            StatusCode = statusCode;
            Details = details;
        }
    }
}