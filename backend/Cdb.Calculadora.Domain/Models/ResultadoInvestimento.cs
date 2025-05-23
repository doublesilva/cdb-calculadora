using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cdb.Calculadora.Domain.Models
{
    public class ResultadoInvestimento
    {
        public decimal ValorBruto { get; }
        public decimal ValorLiquido { get; }

        public ResultadoInvestimento(decimal valorBruto, decimal valorLiquido)
        {
            ValorBruto = valorBruto;
            ValorLiquido = valorLiquido;
        }
    }
}
