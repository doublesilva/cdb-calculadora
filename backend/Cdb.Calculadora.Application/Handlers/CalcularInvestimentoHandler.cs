using Cdb.Calculadora.Application.Commands;
using Cdb.Calculadora.Application.DTOs;
using Cdb.Calculadora.Domain.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cdb.Calculadora.Application.Handlers
{
    public class CalcularInvestimentoHandler(ICalculoCdbService calculoService)
    : IRequestHandler<CalcularInvestimentoCommand, ResultadoInvestimentoDto>
    {
        public Task<ResultadoInvestimentoDto> Handle(CalcularInvestimentoCommand request, CancellationToken cancellationToken)
        {
            ResultadoInvestimentoDto resultado = calculoService.Calcular(request.Dto.ValorInicial, request.Dto.PrazoMeses);
            return Task.FromResult(resultado);
        }
    }
}
