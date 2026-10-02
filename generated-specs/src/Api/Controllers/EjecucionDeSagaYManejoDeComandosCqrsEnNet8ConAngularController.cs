namespace transactionalsystemdotnetangular.Api.Controllers;

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using transactionalsystemdotnetangular.Domain.EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngular; // commands, queries and read models live with the contract

[ApiController]
[Route("api/v1/[controller]")]
public class EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularController : ControllerBase
{
    private readonly IMediator _mediator;

    public EjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEjecucionDeSagaYManejoDeComandosCqrsEnNet8ConAngularCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}
