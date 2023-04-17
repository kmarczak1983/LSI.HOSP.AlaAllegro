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

        [HttpGet("{id}")]
        public Task<IActionResult> GetUserById(int id, CancellationToken cancellationToken)
           => ExecuteQuery(new GetUserByIdQuery(id), cancellationToken);

        [HttpPut]
        [AllowAnonymous]
        public Task<IActionResult> UpdateUser([FromBody] CreateUserCommand command, CancellationToken cancellation)
            => ExecuteCommandNoContent(command, cancellation);

        [HttpPost("login")]
        public Task<IActionResult> LoginUser([FromBody] LoginUserCommand command, CancellationToken cancellation)
            => ExecuteCommand(command, cancellation);
    }
}
