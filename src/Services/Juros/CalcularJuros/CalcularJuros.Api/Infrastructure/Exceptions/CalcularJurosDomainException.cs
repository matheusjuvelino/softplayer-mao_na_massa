using System;

namespace CalcularJuros.Api.Infrastructure.Exceptions
{
    public class CalcularJurosDomainException : Exception
    {
        public CalcularJurosDomainException()
        { }

        public CalcularJurosDomainException(string message)
            : base(message)
        { }

        public CalcularJurosDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
