using Cdb.Calculadora.Domain.Models;

namespace Cdb.Calculadora.Domain.Services
{
    public class CalculoCdbService : ICalculoCdbService
    {
        private const decimal CDI = 0.009m;
        private const decimal TB = 1.08m;

        public ResultadoInvestimento Calcular(decimal valorInicial, int prazoMeses)
        {
            decimal valorFinal = valorInicial;

            for (int i = 0; i < prazoMeses; i++)
                valorFinal *= (1 + (CDI * TB));

            valorFinal = Math.Round(valorFinal, 2); // ⬅️ importante

            var rendimento = Math.Round(valorFinal - valorInicial, 2);
            var aliquota = ObterAliquota(prazoMeses);
            var imposto = Math.Round(rendimento * aliquota, 2);

            return new ResultadoInvestimento(
                valorBruto: valorFinal,
                valorLiquido: Math.Round(valorFinal - imposto, 2)
            );
        }


        private static decimal ObterAliquota(int meses) =>
            meses <= 6 ? 0.225m :
            meses <= 12 ? 0.20m :
            meses <= 24 ? 0.175m : 0.15m;
    }
}
