using Cdb.Calculadora.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Cdb.Calculadora.Application.Commands;

namespace Cdb.Calculadora.Tests.Integration
{
    public class InvestimentoApiExceptionTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public InvestimentoApiExceptionTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact(DisplayName = "Deve retornar 500 e JSON padronizado se houver exceção no handler")]
        public async Task Calcular_DeveRetornarErro500_SeExcecaoLancada()
        {
            // Arrange: mocka o IMediator para lançar exceção
            var mockMediator = new Mock<IMediator>();
            mockMediator
                        .Setup(m => m.Send(It.IsAny<CalcularInvestimentoCommand>(), It.IsAny<CancellationToken>()))
                        .ThrowsAsync(new InvalidOperationException("Erro simulado no handler"));


            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Remove todos os IMediator registrados antes
                    services.RemoveAll<IMediator>();

                    // Injeta o mock como Singleton
                    services.AddSingleton(mockMediator.Object);
                });
            }).CreateClient();

            var dto = new CalcularInvestimentoDto
            {
                ValorInicial = 1000,
                PrazoMeses = 12
            };

            // Act
            var response = await client.PostAsJsonAsync("/api/investimento/calcular", dto);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);

            var json = await response.Content.ReadAsStringAsync();
            json.Should().Contain("Erro interno no servidor");
            json.Should().Contain("Erro simulado no handler");
        }
    }
}
