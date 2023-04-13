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
        public Task<IActionResult> GetAuctionById(Guid id, CancellationToken cancellationToken)
            => ExecuteQuery(new GetAuctionByIdQuery(id), cancellationToken);

        //[HttpPut]
        //public Task<IActionResult> Update(UpdateRoomCommand command, CancellationToken token)
          //  => ExecuteCommand(command, token);
    }
}
