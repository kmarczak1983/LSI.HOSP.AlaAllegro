using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using System;
using MediatR;
using LSI.HOSP.AlaAllegro.Application.Auctions.Queries;
using System.Collections.Generic;
using System.Security.Permissions;

namespace LSI.HOSP.AlaAllegro.Web.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionController : BaseCqrsController
    {
        public AuctionController(IMediator mediator) : base(mediator)
        {
        }

       
        [HttpGet("{id}")]
        public Task<IActionResult> GetAuctionById(int id, CancellationToken cancellationToken)
            => ExecuteQuery(new GetAuctionByIdQuery(id), cancellationToken);
    }
}
