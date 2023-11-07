using System;

namespace OrderProcessingApi.Exceptions
{
    public class UnauthorizedException: ApplicationException
    {
        public UnauthorizedException() : base() { }

        public UnauthorizedException(string message) : base(message) { }

        public UnauthorizedException(string message, Exception exception) : base(message, exception) { }
    }
}
