using LSI.HOSP.AlaAllegro.Domain.Entities.Auctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LSI.HOSP.AlaAllegro.Infrastructure.DataAccess.Interfaces
{
    public interface IPurchaseOfferRepository : IRepository<PurchaseOffer>
    {
        Task<PurchaseOffer> GetLast(Guid auctionId, CancellationToken cancellationToken);
    }
}
