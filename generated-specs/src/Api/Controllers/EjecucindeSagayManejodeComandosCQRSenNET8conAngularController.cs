namespace transactionalsystemdotnetangular.Api.Controllers;

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using transactionalsystemdotnetangular.Application.Commands;

[ApiController]
[Route("api/v1/[controller]")]
public class EjecucindeSagayManejodeComandosCQRSenNET8conAngularController : ControllerBase
{
    private readonly IMediator _mediator;

    public EjecucindeSagayManejodeComandosCQRSenNET8conAngularController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEjecucindeSagayManejodeComandosCQRSenNET8conAngularCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}
