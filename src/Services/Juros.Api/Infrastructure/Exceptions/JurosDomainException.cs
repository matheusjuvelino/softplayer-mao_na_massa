using System;

namespace Juros.Api.Infrastructure.Exceptions
{
    public class JurosDomainException : Exception
    {
        public JurosDomainException()
        { }

        public JurosDomainException(string message)
            : base(message)
        { }

        public JurosDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
