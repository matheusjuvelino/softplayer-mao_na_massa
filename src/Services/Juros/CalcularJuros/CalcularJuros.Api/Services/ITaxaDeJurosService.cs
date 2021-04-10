using System.Threading.Tasks;

namespace CalcularJuros.Api.Services
{
    public interface ITaxaDeJurosService
    {
        Task<decimal> ObterTaxaDeJuros();
    }
}
