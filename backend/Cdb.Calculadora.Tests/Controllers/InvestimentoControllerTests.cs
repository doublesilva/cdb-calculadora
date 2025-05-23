using Cdb.Calculadora.Api.Controllers;
using Cdb.Calculadora.Application.Commands;
using Cdb.Calculadora.Application.DTOs;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cdb.Calculadora.Tests.Controllers
{
    public class InvestimentoControllerTests
    {
        [Fact(DisplayName = "POST /calcular deve retornar 200 OK com resultado esperado")]
        public async Task Calcular_DeveRetornarResultadoOk()
        {
            // Arrange
            var dtoEntrada = new CalcularInvestimentoDto
            {
                ValorInicial = 1000,
                PrazoMeses = 12
            };

            var resultadoEsperado = new ResultadoInvestimentoDto(1123.08m, 1098.47m);
        

            var mockMediator = new Mock<IMediator>();
            mockMediator
                .Setup(m => m.Send(It.IsAny<CalcularInvestimentoCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(resultadoEsperado);

            var controller = new InvestimentoController(mockMediator.Object);

            // Act
            var result = await controller.Calcular(dtoEntrada);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var retorno = okResult.Value.Should().BeOfType<ResultadoInvestimentoDto>().Subject;

            retorno.ValorBruto.Should().Be(1123.08m);
            retorno.ValorLiquido.Should().Be(1098.47m);

            mockMediator.Verify(m => m.Send(It.IsAny<CalcularInvestimentoCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
