using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using System.Security.Permissions;
using LSI.HOSP.AlaAllegro.Application.Users.Commands;
using System;
using LSI.HOSP.AlaAllegro.Application.Auctions.Queries;
using LSI.HOSP.AlaAllegro.Application.Users.Queries;
using Microsoft.AspNetCore.Authorization;

namespace LSI.HOSP.AlaAllegro.Web.Controllers.V1
{    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : BaseCqrsController
    {
        public UserController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public Task<IActionResult> GetUser([FromQuery] GetUserQuery query, CancellationToken cancellationToken)
           => ExecuteQuery(query, cancellationToken);

        [HttpGet("all")]
        public Task<IActionResult> GetUsersAll([FromQuery] GetUsersAllQuery query, CancellationToken cancellationToken)
           => ExecuteQuery(query, cancellationToken);

        [HttpPut]
        [AllowAnonymous]
        public Task<IActionResult> UpdateUser([FromBody] CreateUpdateUserCommand command, CancellationToken cancellation)
            => ExecuteCommandNoContent(command, cancellation);

        [HttpPost("login")]
        [AllowAnonymous]
        public Task<IActionResult> LoginUser([FromBody] LoginUserCommand command, CancellationToken cancellation)
            => ExecuteCommand(command, cancellation);
    }
}
