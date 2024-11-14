namespace Project.Core.Exceptions
{
    public class ApiExceptionResponse : Exception
    {
        public int StatusCode { get; set; }
        public string Details { get; set; }
        public ApiExceptionResponse(int statusCode, string message, string details) : base(message)
        {
            StatusCode = statusCode;
            Details = details;
        }
    }
}