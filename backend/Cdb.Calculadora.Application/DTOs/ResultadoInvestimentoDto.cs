using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Cdb.Calculadora.Application.DTOs
{
    public record ResultadoInvestimentoDto(decimal ValorBruto, decimal ValorLiquido)
    {
        public static implicit operator Domain.Models.ResultadoInvestimento(ResultadoInvestimentoDto resultado)
        {
            return new Domain.Models.ResultadoInvestimento(resultado.ValorBruto, resultado.ValorLiquido);
        }

        public static implicit operator ResultadoInvestimentoDto(Domain.Models.ResultadoInvestimento resultado)
        {
            return new ResultadoInvestimentoDto(resultado.ValorBruto, resultado.ValorLiquido);
        }
    }
}
