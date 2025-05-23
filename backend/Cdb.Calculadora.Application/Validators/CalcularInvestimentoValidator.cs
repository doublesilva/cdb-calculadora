using Cdb.Calculadora.Application.Commands;
using Cdb.Calculadora.Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cdb.Calculadora.Application.Validators
{
    public class CalcularInvestimentoCommandValidator : AbstractValidator<CalcularInvestimentoCommand>
    {
        public CalcularInvestimentoCommandValidator()
        {
            RuleFor(x => x.Dto)
               .NotNull().WithMessage("O objeto calcular investimento não pode ser nulo.");
            RuleFor(x => x.Dto.ValorInicial)
                .GreaterThan(0).WithMessage("O valor inicial deve ser maior que zero.");

            RuleFor(x => x.Dto.PrazoMeses)
                .GreaterThan(1).WithMessage("O prazo deve ser maior que 1 mês.");
        }
    }
}
