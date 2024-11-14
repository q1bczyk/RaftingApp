namespace api
{
    public class ApiControlledException : Exception
    {
        public ApiControlledException(string message) : base(message){}
        public ApiControlledException(string message, Exception innerException) : base(message, innerException){}
    }
}