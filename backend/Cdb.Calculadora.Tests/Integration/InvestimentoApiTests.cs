using Cdb.Calculadora.Application.DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;

namespace Cdb.Calculadora.Tests.Integration
{
    public class InvestimentoApiTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public InvestimentoApiTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.WithWebHostBuilder(builder => { }).CreateClient();
        }

        [Theory(DisplayName = "POST /api/investimento/calcular deve retornar 400 para combinações inválidas")]
        [InlineData(0, 12)]
        [InlineData(1000, 0)]
        [InlineData(0, 0)]
        public async Task Post_Calcular_EntradaInvalida_DeveRetornar400(decimal valorInicial, int prazoMeses)
        {
            // Arrange
            var dto = new CalcularInvestimentoDto
            {
                ValorInicial = valorInicial,
                PrazoMeses = prazoMeses
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/investimento/calcular", dto);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact(DisplayName = "POST /api/investimento/calcular deve retornar 400 para entrada inválida")]
        public async Task Post_Calcular_DeveRetornar400_SeInvalido()
        {
            // Arrange: DTO com valores inválidos
            var dto = new CalcularInvestimentoDto
            {
                ValorInicial = 0,
                PrazoMeses = 0
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/investimento/calcular", dto);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var body = await response.Content.ReadAsStringAsync();

            // Opcional: verificar se contém mensagens de erro
            body.Should().Contain("maior que zero")
                .And.Contain("maior que 1");
        }

        [Fact(DisplayName = "POST /api/investimento/calcular deve retornar 400 se body for null com JSON")]
        public async Task Post_Calcular_DeveRetornar400_SeBodyForNull()
        {
            var content = new StringContent("null", System.Text.Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/investimento/calcular", content);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

    }
}
