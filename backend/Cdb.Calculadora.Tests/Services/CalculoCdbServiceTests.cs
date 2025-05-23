using Cdb.Calculadora.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cdb.Calculadora.Tests.Services
{
    public class CalculoCdbServiceTests
    {
        private readonly CalculoCdbService _service = new();

        [Theory(DisplayName = "Deve calcular investimento com alíquota correta para diferentes prazos")]
        [InlineData(1000, 3, 0.225)]   // até 6 meses
        [InlineData(1000, 12, 0.20)]   // até 12 meses
        [InlineData(1000, 20, 0.175)]  // até 24 meses
        [InlineData(1000, 30, 0.15)]   // acima de 24 meses
        public void Calcular_DeveRetornarValoresEsperados(decimal valorInicial, int prazoMeses, decimal aliquotaEsperada)
        {
            // Act
            var resultado = _service.Calcular(valorInicial, prazoMeses);

            // Assert - Valor Bruto
            Assert.True(resultado.ValorBruto > valorInicial, "O valor bruto deve ser maior que o valor inicial");

            // Assert - Valor Líquido
            var rendimento = resultado.ValorBruto - valorInicial;
            var imposto = rendimento * aliquotaEsperada;
            var valorLiquidoEsperado = resultado.ValorBruto - imposto;

            Assert.Equal(valorLiquidoEsperado, resultado.ValorLiquido, 2);
        }

        [Fact(DisplayName = "Valor final deve ser igual ao valor inicial quando prazo for zero")]
        public void Calcular_PrazoZero_DeveRetornarValorInicial()
        {
            var resultado = _service.Calcular(1000, 0);

            Assert.Equal(1000, resultado.ValorBruto);
            Assert.Equal(1000, resultado.ValorLiquido);
        }
    }
}
