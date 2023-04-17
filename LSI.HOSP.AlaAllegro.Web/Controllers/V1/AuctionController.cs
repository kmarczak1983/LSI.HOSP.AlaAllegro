using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using System;
using MediatR;
using LSI.HOSP.AlaAllegro.Application.Auctions.Queries;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Security;
using Microsoft.AspNetCore.Authorization;
using LSI.HOSP.AlaAllegro.Application.Users.Commands;
using LSI.HOSP.AlaAllegro.Application.Auctions.Commands;

namespace LSI.HOSP.AlaAllegro.Web.Controllers.V1
{
    [Route("api/[controller]")]    
    [ApiController]
    [Authorize]

    public class AuctionController : BaseCqrsController
    {
        public AuctionController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("Auctions")]
        public Task<IActionResult> GetAuctions([FromQuery] GetAuctionsQuery query, CancellationToken cancellation)
           => ExecuteQuery(query, cancellation);


        [HttpGet("{id}")]
        public Task<IActionResult> GetAuctionById([FromRoute] Guid id, CancellationToken cancellationToken)
            => ExecuteQuery(new GetAuctionByIdQuery(id), cancellationToken);

        
        [HttpPut]
        public Task<IActionResult> CreataAuction([FromBody] CreateUpdateAuctionCommand command, CancellationToken cancellation)
            => ExecuteCommandNoContent(command, cancellation);

        [HttpPut("{id}")]
        public Task<IActionResult> UpdateAuction([FromBody] CreateUpdateAuctionCommand command, [FromRoute] Guid id, CancellationToken cancellation)
            => ExecuteCommandNoContent(new CreateUpdateAuctionCommand(id) { Title = command.Title, Body = command.Body, InitialPrice = command.InitialPrice }, cancellation);
    }
}
