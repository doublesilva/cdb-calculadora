using Cdb.Calculadora.Domain.Models;

namespace Cdb.Calculadora.Domain.Services
{
    public interface ICalculoCdbService
    {
        ResultadoInvestimento Calcular(decimal valorInicial, int prazoMeses);
    }
}
