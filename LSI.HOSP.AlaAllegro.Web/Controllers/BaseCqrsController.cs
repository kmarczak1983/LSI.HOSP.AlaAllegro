using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using LSI.HOSP.AlaAllegro.Application.Users.Commands;
using System;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace LSI.HOSP.AlaAllegro.Web.Controllers
{
    public abstract class BaseCqrsController : ControllerBase
    {
        protected readonly IMediator _mediator;

        protected BaseCqrsController(IMediator mediator)
            => _mediator = mediator;

        protected async Task<IActionResult> ExecuteQuery<TQuery>(IRequest<TQuery> query, CancellationToken cancellationToken)
            => Ok(await _mediator.Send(query, cancellationToken));

        protected async Task<IActionResult> ExecuteCommand(IRequest command, CancellationToken cancellationToken)
                    => Ok(await _mediator.Send(command, cancellationToken));

        protected async Task<IActionResult> ExecuteCommand<TCommand>(TCommand command, CancellationToken cancellation)
            where TCommand : IRequest<int>
            => Ok(await _mediator.Send(command, cancellation));

        protected async Task<IActionResult> ExecuteCommand<TCommand>(IRequest<TCommand> command, CancellationToken cancellation)
            => Ok(await _mediator.Send(command, cancellation));

        protected async Task<IActionResult> ExecuteCommandNoContent<TCommand>(IRequest<TCommand> command, CancellationToken cancellation)
        { 
            await _mediator.Send(command, cancellation);
            return NoContent();
        }


    }
}
