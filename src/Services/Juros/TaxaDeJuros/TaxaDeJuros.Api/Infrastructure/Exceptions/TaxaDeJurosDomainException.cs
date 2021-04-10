using System;

namespace TaxaDeJuros.Api.Infrastructure.Exceptions
{
    public class TaxaDeJurosDomainException : Exception
    {
        public TaxaDeJurosDomainException()
        { }

        public TaxaDeJurosDomainException(string message)
            : base(message)
        { }

        public TaxaDeJurosDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
