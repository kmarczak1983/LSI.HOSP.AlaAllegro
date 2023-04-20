using LSI.HOSP.AlaAllegro.Application.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using LSI.HOSP.AlaAllegro.Application.PurchaseOffers.Commands;

namespace LSI.HOSP.AlaAllegro.Web.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BidController : BaseCqrsController
    {
        public BidController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("")]
        public Task<IActionResult> AddPurchaseOffer([FromBody] AddPurchaseOfferCommand command, CancellationToken cancellation)
            => ExecuteCommandNoContent(command, cancellation);
    }
}
