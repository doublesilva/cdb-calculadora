using Cdb.Calculadora.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cdb.Calculadora.Application.Commands
{
    public record CalcularInvestimentoCommand(CalcularInvestimentoDto Dto) : IRequest<ResultadoInvestimentoDto>;
}
