using Cdb.Calculadora.Application.Commands;
using Cdb.Calculadora.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cdb.Calculadora.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvestimentoController(IMediator mediator) : ControllerBase
    {
        [HttpPost("calcular")]
        public async Task<IActionResult> Calcular([FromBody] CalcularInvestimentoDto dto)
        {
            if (dto is null)
                return BadRequest();
            var resultado = await mediator.Send(new CalcularInvestimentoCommand(dto));
            return Ok(resultado);
        }
    }
}
