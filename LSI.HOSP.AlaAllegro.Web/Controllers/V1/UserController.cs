using LSI.HOSP.AlaAllegro.Application.Auctions.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using System.Security.Permissions;

namespace LSI.HOSP.AlaAllegro.Web.Controllers.V1
{
    public class UserController : BaseCqrsController
    {
        public UserController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPut]
        public Task<IActionResult> UpdateGuest([FromBody] CreateUserCommand command, CancellationToken cancellation)
            => ExecuteCommand(command, cancellation);
    }
}
