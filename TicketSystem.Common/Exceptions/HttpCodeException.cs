namespace TicketSystem.Common.Exceptions
{
    public class HttpCodeException : Exception
    {
        public int StatusCodes { get; set; }

        public HttpCodeException(int statusCodes)
        {
            StatusCodes = statusCodes;
        }
    }
}
