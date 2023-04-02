using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;

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
    }
}
