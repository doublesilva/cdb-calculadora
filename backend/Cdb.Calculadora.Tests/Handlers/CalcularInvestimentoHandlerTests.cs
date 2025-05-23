using Cdb.Calculadora.Application.Commands;
using Cdb.Calculadora.Application.DTOs;
using Cdb.Calculadora.Application.Handlers;
using Cdb.Calculadora.Domain.Models;
using Cdb.Calculadora.Domain.Services;
using Moq;

namespace Cdb.Calculadora.Tests.Handlers
{
    public class CalcularInvestimentoHandlerTests
    {
        [Fact(DisplayName = "Deve retornar resultado corretamente ao chamar o serviço de cálculo")]
        public async Task Handle_DeveRetornarResultadoInvestimentoDto()
        {
            // Arrange
            var dtoEntrada = new CalcularInvestimentoDto
            {
                ValorInicial = 1000m,
                PrazoMeses = 12
            };

            var resultadoEsperado = new ResultadoInvestimento(1123.08m, 1098.47m);

            var mockService = new Mock<ICalculoCdbService>();
            mockService
                .Setup(s => s.Calcular(dtoEntrada.ValorInicial, dtoEntrada.PrazoMeses))
                .Returns(resultadoEsperado);

            var handler = new CalcularInvestimentoHandler(mockService.Object);
            var command = new CalcularInvestimentoCommand(dtoEntrada);

            // Act
            var resultado = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(resultadoEsperado.ValorBruto, resultado.ValorBruto);
            Assert.Equal(resultadoEsperado.ValorLiquido, resultado.ValorLiquido);

            mockService.Verify(s => s.Calcular(1000m, 12), Times.Once);
        }

        [Fact(DisplayName = "Deve chamar o serviço com os parâmetros corretos")]
        public async Task Handle_DeveChamarServicoComParametrosCorretos()
        {
            // Arrange
            var mockService = new Mock<ICalculoCdbService>();

            var dto = new CalcularInvestimentoDto
            {
                ValorInicial = 2000,
                PrazoMeses = 6
            };

            var resultadoFake = new ResultadoInvestimento(2100m, 2050m);

            mockService.Setup(s => s.Calcular(It.IsAny<decimal>(), It.IsAny<int>()))
                       .Returns(resultadoFake);

            var handler = new CalcularInvestimentoHandler(mockService.Object);
            var command = new CalcularInvestimentoCommand(dto);

            // Act
            var resultado = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(2100m, resultado.ValorBruto);
            Assert.Equal(2050m, resultado.ValorLiquido);

            mockService.Verify(s => s.Calcular(2000, 6), Times.Once);
        }
    }
}
